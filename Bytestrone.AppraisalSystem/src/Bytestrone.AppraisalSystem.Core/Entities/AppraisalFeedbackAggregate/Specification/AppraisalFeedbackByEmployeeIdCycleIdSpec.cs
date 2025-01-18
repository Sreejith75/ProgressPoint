using Ardalis.Specification;

namespace Bytestrone.AppraisalSystem.Core.Entities.AppraisalFeedbackAggregate.Specification;

public class AppraisalFeedbackByEmployeeIdAndCycleIdSpec : Specification<AppraisalFeedback>
{
    public AppraisalFeedbackByEmployeeIdAndCycleIdSpec(int employeeId, int cycleId)
    {
        Query.Where(af => af.EmployeeId == employeeId && af.CycleId == cycleId)
             .Include(af=>af.AppraisalSummary)
             .Include(af => af.Employee)
                 .ThenInclude(e => e!.Role)
                 .ThenInclude(r => r!.Department)
             .Include(af => af.FeedbackDetails.Where(fd => fd.Status == FeedbackStatus.Pending | fd.Status==FeedbackStatus.UnderReview))
                 .ThenInclude(fd => fd.Question)
                 .ThenInclude(pi => pi!.Indicator)         
                 .ThenInclude(i => i.PerformanceFactor); 
    }
}
