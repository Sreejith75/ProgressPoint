using Ardalis.Result;
using Ardalis.SharedKernel;
using Bytestrone.AppraisalSystem.UseCases.PerformanceFactors;
using Bytestrone.AppraisalSystem.UseCases.PerformanceFactors.List;

namespace Bytestrone.AppraisalSystem.UseCases.Contributors.List;

public class ListPerformanceFactorsHandler(IListPerformanceFactorsQueryService _query)
  : IQueryHandler<ListPerformancefactorsQuery, Result<IEnumerable<PerformanceFactorWithIndicatorsDTO>>>
{
  public async Task<Result<IEnumerable<PerformanceFactorWithIndicatorsDTO>>> Handle(ListPerformancefactorsQuery request, CancellationToken cancellationToken)
  {
    var result = await _query.ListAsync();

    return Result.Success(result);
  }
}
