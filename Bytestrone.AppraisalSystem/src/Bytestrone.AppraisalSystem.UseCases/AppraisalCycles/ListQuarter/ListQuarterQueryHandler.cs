using Ardalis.Result;
using Ardalis.SharedKernel;
using Bytestrone.AppraisalSystem.Core.Entities.AppraisalCycleAggregate;

namespace Bytestrone.AppraisalSystem.UseCases.AppraisalCycles.ListQuarter;
public class ListQuarterQueryHandler : IQueryHandler<ListQuarterQuery, Result<List<QuarterDTO>>>
{
    public Task<Result<List<QuarterDTO>>> Handle(ListQuarterQuery request, CancellationToken cancellationToken)
    {
        var quarters = Quarter.List.Select(q => new QuarterDTO
        {
            Quarter = q.Name,
            QuarterId = q.Value
        }).ToList();

        return Task.FromResult(Result<List<QuarterDTO>>.Success(quarters));
    }
}