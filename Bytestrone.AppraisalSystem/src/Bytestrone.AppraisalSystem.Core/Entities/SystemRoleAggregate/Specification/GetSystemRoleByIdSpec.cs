using Ardalis.Specification;
using Bytestrone.AppraisalSystem.Core.SystemRoleAggregate;

namespace Bytestrone.AppraisalSystem.Core.Entities.SystemRoleAggregate.Specification;
public class GetSystemRoleByIdSpec : Specification<SystemRole>
{
    public GetSystemRoleByIdSpec(int systemRoleId)
    {
        Query.Where(sr => sr.Id == systemRoleId);
    }
}
