using Bytestrone.AppraisalSystem.Core.PermissionAggregate;

namespace Bytestrone.AppraisalSystem.Core.SystemRoleAggregate;

public class SystemRolePermission(int systemRoleId, int permissionId)
{
  public int SystemRoleId { get; private set; } = systemRoleId;
  public virtual SystemRole SystemRole { get; private set; }=default!;

  public int PermissionId { get; private set; } = permissionId;
  public virtual Permission Permission { get; private set; }=default!;
}
