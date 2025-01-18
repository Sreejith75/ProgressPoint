
using Bytestrone.AppraisalSystem.UseCases.Questions;
namespace Bytestrone.AppraisalSystem.UseCases.PerformanceIndicators;
public class PerformanceIndicatorDTO
{
    public int IndicatorId { get; set; }
    public string? IndicatorName { get; set; }
    public decimal Weightage { get; set; }
    public List<QuestionsDTO> Questions { get; set; } = new();
}