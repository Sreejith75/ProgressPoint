using Ardalis.Specification;

namespace Bytestrone.AppraisalSystem.Core.Entities.AppraisalSummeryAggregate.Specification;
public class GetSummeryByEmployeeIdCycleIdSpec : Specification<AppraisalSummary>
{
    public GetSummeryByEmployeeIdCycleIdSpec(int EmployeeId, int CycleId)
    {
        Query.Where(x => x.EmployeeId == EmployeeId && x.CycleId == CycleId);
    }
}