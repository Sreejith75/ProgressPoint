using Ardalis.Specification;

namespace Bytestrone.AppraisalSystem.Core.Entities.AppraisalFormAggregate.Specification;

public class AppraisalFormWithDetailsSpec : Specification<AppraisalForm>
{
    public AppraisalFormWithDetailsSpec()
    {
        Query
            .Include(f => f.EmployeeRole);
    }
}
