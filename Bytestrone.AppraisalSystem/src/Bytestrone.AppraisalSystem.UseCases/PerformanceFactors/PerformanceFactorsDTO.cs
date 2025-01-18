using Bytestrone.AppraisalSystem.UseCases.PerformanceIndicators;
namespace Bytestrone.AppraisalSystem.UseCases.PerformanceFactors;
public class PerformanceFactorsDTO
{
    public int FactorId { get; set; }
    public string? FactorName { get; set; }
    public List<PerformanceIndicatorDTO> Indicators { get; set; } = new();
}