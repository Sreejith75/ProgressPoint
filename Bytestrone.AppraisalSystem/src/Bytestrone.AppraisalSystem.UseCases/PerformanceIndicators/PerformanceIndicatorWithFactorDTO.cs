using Bytestrone.AppraisalSystem.UseCases.PerformanceFactors;

namespace Bytestrone.AppraisalSystem.UseCases.PerformanceIndicators;

public record PerformanceIndicatorWithFactorDTO
{
    public int IndicatorId { get; init; }
    public string? IndicatorName { get; init; }
    public decimal Weightage { get; init; }
    public PerformanceFactorDTO? Factor { get; init; }

    public PerformanceIndicatorWithFactorDTO(
        int indicatorId,
        string? indicatorName,
        decimal weightage,
        PerformanceFactorDTO? factor)
    {
        IndicatorId = indicatorId;
        IndicatorName = indicatorName;
        Weightage = weightage;
        Factor = factor;
    }
}
