using Ardalis.GuardClauses;
using Ardalis.SharedKernel;
using Bytestrone.AppraisalSystem.Core.EmployeeAggregate;
using Bytestrone.AppraisalSystem.Core.PermissionAggregate;

namespace Bytestrone.AppraisalSystem.Core.SystemRoleAggregate;

public class SystemRole(string roleName, string description) : EntityBase, IAggregateRoot
{
    public string RoleName { get; private set; } = Guard.Against.NullOrEmpty(roleName, nameof(roleName));
    public string Description { get; private set; } = Guard.Against.NullOrEmpty(description, nameof(description));
    private readonly List<EmployeeSystemRole> _employeeSystemRoles = new();
    public IReadOnlyCollection<EmployeeSystemRole> EmployeeSystemRoles => _employeeSystemRoles.AsReadOnly();

    private readonly List<SystemRolePermission> _systemRolePermissions = new();
    public IReadOnlyCollection<SystemRolePermission> SystemRolePermissions => _systemRolePermissions.AsReadOnly();

    public void AddPermission(Permission permission)
    {
        Guard.Against.Null(permission, nameof(permission));

        if (!_systemRolePermissions.Any(sr => sr.PermissionId == permission.Id))
        {
            _systemRolePermissions.Add(new SystemRolePermission(this.Id, permission.Id));
        }
    }

    public void RemovePermission(Permission permission)
    {
        Guard.Against.Null(permission, nameof(permission));

        var systemRolePermission = _systemRolePermissions.FirstOrDefault(sr => sr.PermissionId == permission.Id);
        if (systemRolePermission != null)
        {
            _systemRolePermissions.Remove(systemRolePermission);
        }
    }

    public IReadOnlyCollection<Permission> GetPermissions()
    {
        var permissions = _systemRolePermissions.Select(sr => sr.Permission).Distinct().ToList();
        return permissions;
    }

    public void UpdateDetails(string roleName, string description)
    {
        RoleName = Guard.Against.NullOrEmpty(roleName, nameof(roleName));
        Description = Guard.Against.NullOrEmpty(description, nameof(description));
    }

    public void UpdatePermissions(List<Permission> newPermissions)
    {
        Guard.Against.Null(newPermissions, nameof(newPermissions));

        var permissionsToRemove = _systemRolePermissions
            .Where(sr => !newPermissions.Any(p => p.Id == sr.PermissionId))
            .ToList();

        foreach (var permissionToRemove in permissionsToRemove)
        {
            _systemRolePermissions.Remove(permissionToRemove);
        }

        foreach (var newPermission in newPermissions)
        {
            if (!_systemRolePermissions.Any(sr => sr.PermissionId == newPermission.Id))
            {
                _systemRolePermissions.Add(new SystemRolePermission(this.Id, newPermission.Id));
            }
        }
    }

}
