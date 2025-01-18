using Ardalis.Result;
using Ardalis.SharedKernel;

namespace Bytestrone.AppraisalSystem.UseCases.Questions.List;
public record ListQuestionQuery : IQuery<Result<IEnumerable<QuestionDTO>>>;