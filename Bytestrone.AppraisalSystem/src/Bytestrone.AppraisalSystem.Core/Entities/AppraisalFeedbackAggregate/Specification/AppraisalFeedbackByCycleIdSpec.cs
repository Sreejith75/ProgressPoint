using Ardalis.Specification;

namespace Bytestrone.AppraisalSystem.Core.Entities.AppraisalFeedbackAggregate.Specification;
public class AppraisalFeedbackByCycleIdSpec:Specification<AppraisalFeedback>
{
    public AppraisalFeedbackByCycleIdSpec(int CycleId)
    {
        Query.Where(af=>af.CycleId==CycleId).Include(af=>af.AppraisalCycle).Include(af=>af.AppraisalSummary);
    }
}