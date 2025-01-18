using Bytestrone.AppraisalSystem.Core.Entities.PerformanceFactorAggregate;

namespace Bytestrone.AppraisalSystem.Web.Questions;
public class CreateQuestionRequest
{
    public const string Route="/Questions";
    public string? QuestionText { get; set; }
    public int IndicatorId { get; set; }
}