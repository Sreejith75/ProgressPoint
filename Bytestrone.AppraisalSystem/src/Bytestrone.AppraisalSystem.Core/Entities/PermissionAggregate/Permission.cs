using Ardalis.GuardClauses;
using Ardalis.SharedKernel;
using Bytestrone.AppraisalSystem.Core.SystemRoleAggregate;

namespace Bytestrone.AppraisalSystem.Core.PermissionAggregate;

public class Permission(string permissionName, string permissionCode) : EntityBase,IAggregateRoot
{
  public string PermissionName { get; private set; } = Guard.Against.NullOrEmpty(permissionName, nameof(permissionName));
  public string PermissionCode { get; private set; } = Guard.Against.NullOrEmpty(permissionCode, nameof(permissionCode));
  public ICollection<SystemRolePermission> SystemRolePermissions { get; private set; } = new List<SystemRolePermission>();
  public void AddSystemRolePermission(SystemRolePermission rolePermission)
  {
    Guard.Against.Null(rolePermission, nameof(rolePermission));
    if (!SystemRolePermissions.Contains(rolePermission))
    {
      SystemRolePermissions.Add(rolePermission);
    }
  }

  public void RemoveSystemRolePermission(SystemRolePermission rolePermission)
  {
    Guard.Against.Null(rolePermission, nameof(rolePermission));
    if (SystemRolePermissions.Contains(rolePermission))
    {
      SystemRolePermissions.Remove(rolePermission);
    }
  }
}
