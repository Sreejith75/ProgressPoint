using Ardalis.GuardClauses;
using Ardalis.SharedKernel;
using Bytestrone.AppraisalSystem.Core.EmployeeAggregate;
using Bytestrone.AppraisalSystem.Core.Interfaces;
using Bytestrone.AppraisalSystem.Core.SystemRoleAggregate;
using Bytestrone.AppraisalSystem.Core.Entities.EmployeeRoleAggregate.Specification;
using Bytestrone.AppraisalSystem.Core.Entities.SystemRoleAggregate.Specification;
using Bytestrone.AppraisalSystem.Core.Entities.EmployeeAggregate.Specification;
using Bytestrone.AppraisalSystem.Core.Entities.EmployeeRoleAggregate;

namespace Bytestrone.AppraisalSystem.Core.Services;
public class CreateEmployeeService(
    IRepository<Employee> employeeRepository,
    IRepository<EmployeeRole> employeeRoleRepository,
    IRepository<SystemRole> systemRoleRepository,
    IHashingService hashingService) : ICreateEmployeeService
{
    private readonly IRepository<Employee> _employeeRepository = Guard.Against.Null(employeeRepository, nameof(employeeRepository));
    private readonly IRepository<EmployeeRole> _employeeRoleRepository = Guard.Against.Null(employeeRoleRepository, nameof(employeeRoleRepository));
    private readonly IRepository<SystemRole> _systemRoleRepository = Guard.Against.Null(systemRoleRepository, nameof(systemRoleRepository));
    private readonly IHashingService _hashingService = Guard.Against.Null(hashingService, nameof(hashingService));

    public async Task<int> CreateEmployeeAsync(
        string firstName,
        string lastName,
        string email,
        string password,
        string? phoneNumber,
        int employeeRoleId,
        IEnumerable<int> systemRoleIds,
        int? appraiserId,
        CancellationToken cancellationToken)
    {
        var employeeRoleSpec = new EmployeeRoleByIdSpec(employeeRoleId);
        var employeeRole = await _employeeRoleRepository.FirstOrDefaultAsync(employeeRoleSpec, cancellationToken);
        Guard.Against.Null(employeeRole, nameof(employeeRole));

        var systemRolesSpec = new SystemRolesByIdsSpec(systemRoleIds);
        var systemRoles = await _systemRoleRepository.ListAsync(systemRolesSpec, cancellationToken);
        Guard.Against.Null(systemRoles, nameof(systemRoles));

        if (systemRoles.Count() != systemRoleIds.Count())
        {
            throw new ArgumentException("One or more system roles are invalid.");
        }

        var passwordHash = _hashingService.HashPassword(password);

        var employee = new Employee(
            firstName,
            lastName,
            email,
            passwordHash,
            employeeRoleId
        );

        if (!string.IsNullOrEmpty(phoneNumber))
        {
            employee.UpdateContactInfo(phoneNumber);
        }

        foreach (var systemRole in systemRoles)
        {
            employee.AddSystemRole(systemRole.Id);
        }

        await _employeeRepository.AddAsync(employee, cancellationToken);


        if (appraiserId.HasValue)
        {
            var appraiserSpec = new EmployeeByIdSpec(appraiserId.Value);

            var appraiser = await _employeeRepository.FirstOrDefaultAsync(appraiserSpec, cancellationToken);

            if (appraiser != null)
            {
                employee.AssignNewAppraiser(appraiser, DateTime.UtcNow);
            }
        }
        await _employeeRepository.UpdateAsync(employee, cancellationToken);
        return employee.Id;
    }
}
