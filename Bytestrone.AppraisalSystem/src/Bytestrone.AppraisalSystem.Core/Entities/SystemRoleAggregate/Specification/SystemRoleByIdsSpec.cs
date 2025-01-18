using Ardalis.Specification;
using Bytestrone.AppraisalSystem.Core.SystemRoleAggregate;

namespace Bytestrone.AppraisalSystem.Core.Entities.SystemRoleAggregate.Specification;
public class SystemRolesByIdsSpec : Specification<SystemRole>
{
    public SystemRolesByIdsSpec(IEnumerable<int> ids)
    {
        Query.Where(role => ids.Contains(role.Id));
    }
}