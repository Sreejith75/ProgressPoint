using Ardalis.GuardClauses;
using Ardalis.SharedKernel;
using Bytestrone.AppraisalSystem.Core.Entities.AppraisalSummeryAggregate;
using Bytestrone.AppraisalSystem.Core.Entities.EmployeeAggregate;
using Bytestrone.AppraisalSystem.Core.Entities.EmployeeRoleAggregate;
using Bytestrone.AppraisalSystem.Core.Interfaces;
using Bytestrone.AppraisalSystem.Core.PermissionAggregate;
using Bytestrone.AppraisalSystem.Core.SystemRoleAggregate;

namespace Bytestrone.AppraisalSystem.Core.EmployeeAggregate;

public class Employee(string firstName, string lastName, string email, string passwordHash, int employeeRoleId) : EntityBase, IAggregateRoot
{
    private readonly List<EmployeeSystemRole> _systemRoles = new();
    public ICollection<AppraisalSummary> AppraisalSummaries { get; private set; } = new List<AppraisalSummary>();

    public string FirstName { get; private set; } = Guard.Against.NullOrEmpty(firstName, nameof(firstName));
    public string LastName { get; private set; } = Guard.Against.NullOrEmpty(lastName, nameof(lastName));
    public string Email { get; private set; } = Guard.Against.NullOrEmpty(email, nameof(email));
    public string? PhoneNumber { get; private set; }
    public string PasswordHash { get; private set; } = Guard.Against.NullOrEmpty(passwordHash, nameof(passwordHash));
    public bool IsActive { get; private set; } = true;
    public int EmployeeRoleId { get; private set; } = employeeRoleId;
    public EmployeeRole? Role { get; private set; }

    public DateTime CreatedOn { get; private set; } = DateTime.UtcNow;
    public DateTime? ModifiedOn { get; private set; } = DateTime.UtcNow;

    public ICollection<EmployeeAppraiserMapping> Appraisees { get; private set; } = new List<EmployeeAppraiserMapping>();
    public ICollection<EmployeeAppraiserMapping> Appraisers { get; private set; } = new List<EmployeeAppraiserMapping>();


    public IReadOnlyCollection<EmployeeSystemRole> SystemRoles => _systemRoles.AsReadOnly();


    public void UpdateContactInfo(string phoneNumber)
    {
        PhoneNumber = Guard.Against.NullOrEmpty(phoneNumber, nameof(phoneNumber));
    }

    public void UpdatePassword(string password, IHashingService hashingService)
    {
        PasswordHash = Guard.Against.NullOrEmpty(password, nameof(password));
        PasswordHash = hashingService.HashPassword(password);
    }

    public void AssignNewAppraiser(Employee appraiser, DateTime effectiveDate)
    {
        Guard.Against.Null(appraiser, nameof(appraiser));
        Guard.Against.Default(effectiveDate, nameof(effectiveDate));

        if (Id == appraiser.Id)
        {
            throw new InvalidOperationException("An employee cannot be their own appraiser.");
        }

        // Create a new mapping
        var newMapping = new EmployeeAppraiserMapping(Id, appraiser.Id, effectiveDate);
    
        // Add to the collections
        Appraisees.Add(newMapping); 
    }



    public void AddSystemRole(int systemRoleId)
    {
        Guard.Against.Null(systemRoleId, nameof(systemRoleId));

        if (!_systemRoles.Any(r => r.SystemRoleId == systemRoleId))
        {
            _systemRoles.Add(new EmployeeSystemRole(Id, systemRoleId));
        }
    }

    public void RemoveSystemRole(int systemRoleId)
    {
        var roleToRemove = _systemRoles.FirstOrDefault(r => r.SystemRoleId == systemRoleId);
        if (roleToRemove != null)
        {
            _systemRoles.Remove(roleToRemove);
        }
    }

    public IEnumerable<EmployeeSystemRole> GetSystemRoles()
    {
        return _systemRoles.AsEnumerable();
    }

    public string GetFullName()
    {
        return firstName + " " + lastName;
    }

    public List<string?> GetPermissions()
    {
        var permissions = SystemRoles
            .Where(esr => esr.SystemRole != null)
            .SelectMany(esr => esr.SystemRole!.SystemRolePermissions.Select(rp => rp.Permission?.PermissionCode))
            .Where(code => !string.IsNullOrEmpty(code))
            .Distinct();

        return permissions.ToList();
    }
    public List<int> GetAppraiseeList()
    {
        var appraiseeIds = Appraisers
            .Where(mapping => mapping.AppraiserId == Id)
            .Select(mapping => mapping.EmployeeId)
            .Distinct()
            .ToList();
        return appraiseeIds;
    }
    public string? GetCurrentManagerName()
    {
        var activeAppraiser = Appraisees
            .Where(mapping => mapping.Status == "Active" && mapping.EmployeeId == Id)
            .OrderByDescending(mapping => mapping.EffectiveDate)
            .FirstOrDefault()?.Appraiser;

        return activeAppraiser?.GetFullName();
    }
}
