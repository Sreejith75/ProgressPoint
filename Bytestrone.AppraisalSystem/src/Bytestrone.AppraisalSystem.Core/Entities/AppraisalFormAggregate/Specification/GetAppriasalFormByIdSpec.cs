using Ardalis.Specification;

namespace Bytestrone.AppraisalSystem.Core.Entities.AppraisalFormAggregate.Specification;

public class GetAppraisalFormByIdSpec : Specification<AppraisalForm>
{
    public GetAppraisalFormByIdSpec(int FormId)
    {
        Query
            .Where(f=>f.Id==FormId);
    }
}
