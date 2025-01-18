using Ardalis.Result;
using Ardalis.SharedKernel;
using Bytestrone.AppraisalSystem.Core.Entities.AppraisalCycleAggregate;
using Bytestrone.AppraisalSystem.UseCases.AppraisalCycles.ListActive;
using System.Linq;

namespace Bytestrone.AppraisalSystem.UseCases.AppraisalCycles.List;
public class ListActiveCyclesQueryHandler(IRepository<AppraisalCycle> repository) : IQueryHandler<ListActiveCyclesQuery, Result<IEnumerable<ActiveCycleDTO>>>
{
    private readonly IRepository<AppraisalCycle> _repository = repository;

    public async Task<Result<IEnumerable<ActiveCycleDTO>>> Handle(ListActiveCyclesQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var cycles = await _repository.ListAsync(cancellationToken);

            var activeCycles = cycles.Where(c => c.Status.Value == CycleStatus.NotStarted)
                                      .Select(c => new ActiveCycleDTO
                                      {
                                          Id = c.Id,
                                          Quarter = c.Quarter.Name,
                                          Year = c.Year,
                                      });

            return Result<IEnumerable<ActiveCycleDTO>>.Success(activeCycles);
        }
        catch (Exception ex)
        {
            return Result<IEnumerable<ActiveCycleDTO>>.Unavailable(ex.Message);
        }
    }
}
