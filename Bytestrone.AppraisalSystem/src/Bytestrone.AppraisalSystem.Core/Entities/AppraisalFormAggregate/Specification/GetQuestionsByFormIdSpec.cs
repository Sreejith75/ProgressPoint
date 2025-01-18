using Ardalis.Specification;
using Bytestrone.AppraisalSystem.Core.Entities.AppraisalFormAggregate;
using Bytestrone.AppraisalSystem.Core.Entities.QuestionAggregate;

namespace Bytestrone.AppraisalSystem.Core.Entities.AppraisalFormAggregate.Specification;
public class GetQuestionsByFormIdSpec : Specification<AppraisalForm>
{
    public GetQuestionsByFormIdSpec(int formId)
    {
        Query.Where(x => x.Id == formId)
            .Include(x => x.FormQuestionMappings)
                .ThenInclude(fqm => fqm.Question);
    }
}