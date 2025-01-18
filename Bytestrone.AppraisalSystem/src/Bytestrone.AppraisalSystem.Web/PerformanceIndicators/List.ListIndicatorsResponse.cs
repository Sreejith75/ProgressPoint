using Bytestrone.AppraisalSystem.UseCases.PerformanceFactors.ListIndicators;

namespace Bytestrone.AppraisalSystem.web.PerformanceIndicators;
public class ListIndicatorsResponse
{
    public string? Message { get; set; }
    public List<FactorDTO>? factors  { get; set; }
}