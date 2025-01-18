using Ardalis.Specification;
using Bytestrone.AppraisalSystem.Core.PermissionAggregate;

namespace Bytestrone.AppraisalSystem.Core.Entities.SystemRoleAggregate.Specification;
public class PermissionByRoleSpecification : Specification<Permission>
{
    public PermissionByRoleSpecification(int roleId)
    {
        Query.Where(p => p.SystemRolePermissions.Any(rp => rp.SystemRoleId == roleId));
    }
}
