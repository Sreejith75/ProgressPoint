using Ardalis.Result;
using Ardalis.SharedKernel;
using Bytestrone.AppraisalSystem.Core.Entities.AppraisalCycleAggregate;
using System.Linq;

namespace Bytestrone.AppraisalSystem.UseCases.AppraisalCycles.List;
public class ListAppraisalCycleQueryHandler(IRepository<AppraisalCycle> repository) : IQueryHandler<ListAppraisalCycleQuery, Result<IEnumerable<AppraisalCycleDTO>>>
{
    private readonly IRepository<AppraisalCycle> _repository = repository;

    public async Task<Result<IEnumerable<AppraisalCycleDTO>>> Handle(ListAppraisalCycleQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var cycles = await _repository.ListAsync(cancellationToken);

            var orderedCycles = cycles
                .OrderBy(c => c.Quarter.Value)
                .ThenBy(c => c.Year)
                .ThenBy(c => c.AppraiseeDateRange.StartDate)
                .ThenBy(c => c.AppraiserDateRange.StartDate)
                .ToList();

            var cycleDtos = orderedCycles.Where(c=>c.Status!=CycleStatus.Completed)
                .Select(cycle => new AppraisalCycleDTO
                {
                    Id = cycle.Id,
                    Quarter = cycle.Quarter.Name,
                    Year = cycle.Year,
                    AppraiseeStartDate = cycle.AppraiseeDateRange.StartDate,
                    AppraiseeEndDate = cycle.AppraiseeDateRange.EndDate,
                    AppraiserStartDate = cycle.AppraiserDateRange.StartDate,
                    AppraiserEndDate = cycle.AppraiserDateRange.EndDate,
                    Status = cycle.Status.Name 
                })
                .ToList();

            return Result<IEnumerable<AppraisalCycleDTO>>.Success(cycleDtos);
        }
        catch (Exception ex)
        {
            return Result<IEnumerable<AppraisalCycleDTO>>.Unavailable(ex.Message);
        }
    }
}
