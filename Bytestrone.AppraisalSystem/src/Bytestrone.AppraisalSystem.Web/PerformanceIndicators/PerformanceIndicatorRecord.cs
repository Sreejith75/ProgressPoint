using Bytestrone.AppraisalSystem.web.PerformanceFactors;

namespace Bytestrone.AppraisalSystem.web.PerformanceIndicators;
public record PerformanceIndicatorRecord(
    int IndicatorId,
    string IndicatorName,
    decimal Weightage,
    PerformanceFactorRecord Factor
);