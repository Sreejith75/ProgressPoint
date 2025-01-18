using Ardalis.Specification;

namespace Bytestrone.AppraisalSystem.Core.Entities.QuestionAggregate.Specification;
public class GetQuestionByIdSpec :Specification<Question>
{
    public GetQuestionByIdSpec(int id)=>Query.Where(q=>q.Id==id);
}