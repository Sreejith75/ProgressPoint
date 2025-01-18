using Ardalis.Result;
using Ardalis.SharedKernel;
namespace Bytestrone.AppraisalSystem.UseCases.PerformanceFactors.List;
public record ListPerformancefactorsQuery() : IQuery<Result<IEnumerable<PerformanceFactorWithIndicatorsDTO>>>;
