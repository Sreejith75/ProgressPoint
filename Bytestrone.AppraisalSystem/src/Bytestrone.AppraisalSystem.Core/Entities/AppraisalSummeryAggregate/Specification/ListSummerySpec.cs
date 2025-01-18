using Ardalis.Specification;

namespace Bytestrone.AppraisalSystem.Core.Entities.AppraisalSummeryAggregate.Specification;

public class ListSummerySpec : Specification<AppraisalSummary>
{
    public ListSummerySpec()
    {
        Query.Include(x => x.AppraisalCycle)
             .Include(x => x.Employee)
                 .ThenInclude(e => e.Role)
                 .ThenInclude(r => r!.Department)
             .Include(x => x.Employee)
                 .ThenInclude(e => e.Appraisers);  
    }
}
