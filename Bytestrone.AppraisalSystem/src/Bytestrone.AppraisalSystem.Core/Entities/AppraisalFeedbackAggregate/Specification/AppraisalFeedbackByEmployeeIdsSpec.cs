using Ardalis.Specification;

namespace Bytestrone.AppraisalSystem.Core.Entities.AppraisalFeedbackAggregate.Specification;

public class AppraisalFeedbackByEmployeeIdsSpec : Specification<AppraisalFeedback>
{
    public AppraisalFeedbackByEmployeeIdsSpec(List<int> employeeIds)
    {
        if (employeeIds != null && employeeIds.Count > 0)
        {
            Query.Include(feedback=>feedback.AppraisalSummary).Where(feedback => employeeIds.Contains(feedback.EmployeeId));
        }
    }
}
