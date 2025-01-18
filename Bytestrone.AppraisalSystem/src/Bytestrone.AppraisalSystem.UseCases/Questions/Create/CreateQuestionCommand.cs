using Ardalis.Result;
using Bytestrone.AppraisalSystem.Core.Entities.PerformanceFactorAggregate;

namespace Bytestrone.AppraisalSystem.UseCases.Questions.Create;
public record CreateQuestionCommand(string QuestionText,int IndicatorId) :Ardalis.SharedKernel.ICommand<Result<int>>
{
    public string? QuestionText { get; set; }=QuestionText;
    public int IndicatorId { get; set; }=IndicatorId;
}
