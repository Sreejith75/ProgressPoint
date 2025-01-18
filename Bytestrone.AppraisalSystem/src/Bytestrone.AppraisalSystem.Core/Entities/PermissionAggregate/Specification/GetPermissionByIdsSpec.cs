using Ardalis.Specification;
using Bytestrone.AppraisalSystem.Core.PermissionAggregate;
namespace Bytestrone.AppraisalSystem.Core.Entities.PermissionAggregate.Specification;
public class GetPermissionByIdsSpec : Specification<Permission>
{

  public GetPermissionByIdsSpec(List<int> permissionIds)
  {
    Query.Where(p=>permissionIds.Contains(p.Id));
  }
}
