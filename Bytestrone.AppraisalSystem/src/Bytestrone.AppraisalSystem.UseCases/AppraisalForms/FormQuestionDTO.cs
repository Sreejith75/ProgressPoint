using Bytestrone.AppraisalSystem.Core.Entities.QuestionAggregate;
using Bytestrone.AppraisalSystem.UseCases.Questions;

namespace Bytestrone.AppraisalSystem.UseCases.AppraisalForms;
public record FormQuestionDTO
{
    public int FormId { get; set; }
    public List<QuestionsDTO> Questions { get; set; } =default!;
}