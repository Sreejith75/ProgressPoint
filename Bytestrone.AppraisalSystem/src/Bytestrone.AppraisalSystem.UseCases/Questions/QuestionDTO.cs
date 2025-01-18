using Bytestrone.AppraisalSystem.Core.Entities.PerformanceIndicatorAggregate;
using Bytestrone.AppraisalSystem.UseCases.PerformanceFactors;
using Bytestrone.AppraisalSystem.UseCases.PerformanceIndicators;

namespace Bytestrone.AppraisalSystem.UseCases.Questions;
public class QuestionDTO
{
    public int QuestionId { get; set; }
    public string? QuestionText { get; set; }
    public PerformanceIndicatorWithFactorDTO? Indicator { get; set; }
}
