using Bytestrone.AppraisalSystem.Core.Entities.PerformanceFactorAggregate;
using Bytestrone.AppraisalSystem.UseCases.PerformanceFactors;
using Bytestrone.AppraisalSystem.web.PerformanceFactors;

namespace Bytestrone.AppraisalSystem.web.PerformanceIndicators;

public record PerformanceIndicatorWithFactorRecord
{
    public int IndicatorId { get; init; }
    public string? IndicatorName { get; init; }
    public decimal Weightage { get; init; }
    public PerformanceFactorRecord? Factor { get; init; }


}
