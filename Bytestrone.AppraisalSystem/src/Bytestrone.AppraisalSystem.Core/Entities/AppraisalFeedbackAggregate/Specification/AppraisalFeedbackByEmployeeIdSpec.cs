using Ardalis.Specification;

namespace Bytestrone.AppraisalSystem.Core.Entities.AppraisalFeedbackAggregate.Specification;
public class AppraisalFeedbackByEmployeeIdSpec : Specification<AppraisalFeedback>
{
    public AppraisalFeedbackByEmployeeIdSpec(int EmployeeId)
    {
        Query.Include(af=>af.AppraisalSummary)
             .Include(af=>af.FeedbackDetails)
             .Include(af=>af.AppraisalSummary)
             .Include(af=>af.AppraisalCycle)
             .Where(af=>af.EmployeeId==EmployeeId);
    }
}