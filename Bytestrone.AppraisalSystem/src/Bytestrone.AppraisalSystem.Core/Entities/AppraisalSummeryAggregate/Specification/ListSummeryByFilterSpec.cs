using Ardalis.Specification;

namespace Bytestrone.AppraisalSystem.Core.Entities.AppraisalSummeryAggregate.Specification;

public class ListSummeryByFilterSpec : Specification<AppraisalSummary>
{
    public ListSummeryByFilterSpec(int? roleId, int? departmentId)
    {
        Query.Include(x => x.AppraisalCycle)
             .Include(x => x.Employee)
                 .ThenInclude(e => e.Role)
                 .ThenInclude(r => r!.Department);

        // Apply dynamic filters based on the passed arguments
        if (roleId.HasValue)
        {
            Query.Where(x => x.Employee.Role!.Id == roleId);
        }

        if (departmentId.HasValue)
        {
            Query.Where(x => x.Employee.Role!.Department!.Id == departmentId);
        }
    }
}
