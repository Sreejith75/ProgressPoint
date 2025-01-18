using Ardalis.Result;
using Ardalis.SharedKernel;

namespace Bytestrone.AppraisalSystem.UseCases.PerformanceFactors.ListIndicators;
public record ListIndicatorsQuery(): IQuery<Result<List<FactorDTO>>>;