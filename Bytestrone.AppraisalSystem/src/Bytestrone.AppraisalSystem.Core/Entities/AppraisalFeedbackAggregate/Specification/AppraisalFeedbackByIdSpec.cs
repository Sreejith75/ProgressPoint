using Ardalis.Specification;

namespace Bytestrone.AppraisalSystem.Core.Entities.AppraisalFeedbackAggregate.Specification;

public class AppraisalFeedbackByIdSpec : Specification<AppraisalFeedback>
{
    public AppraisalFeedbackByIdSpec(int feedbackId)
    {
        Query.Where(af => af.Id == feedbackId) // Assuming `Id` is the primary key for AppraisalFeedback
             .Include(af => af.Employee)
                 .ThenInclude(e => e!.Role)
                 .ThenInclude(r => r!.Department)
             .Include(af => af.FeedbackDetails)
                 .ThenInclude(fd => fd.Question)
                 .ThenInclude(q => q!.Indicator)            // Assuming `Indicator` is a navigation property
                 .ThenInclude(i => i.PerformanceFactor)
                 .ThenInclude(i=>i.DepartmentPerformanceFactors);   // Assuming `PerformanceFactor` is a navigation property
    }
}
