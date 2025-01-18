using Ardalis.Result;
using Ardalis.SharedKernel;
using Bytestrone.AppraisalSystem.Core.Entities.AppraisalCycleAggregate;

namespace Bytestrone.AppraisalSystem.UseCases.AppraisalCycles.GetActive;
public class GetActiveAppraisalCycleQueryHandler(IRepository<AppraisalCycle> repository)
    : IQueryHandler<GetActiveAppraisalCycleQuery, Result<AppraisalCycleDTO>>
{
    private readonly IRepository<AppraisalCycle> _repository = repository;

    public async Task<Result<AppraisalCycleDTO>> Handle(GetActiveAppraisalCycleQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var cycles = await _repository.ListAsync(cancellationToken);

            var inProgressCycle = cycles
                .Where(c => c.Status == CycleStatus.InProgress &&
                           DateOnly.FromDateTime(DateTime.UtcNow) >= c.AppraiseeDateRange.StartDate &&
                           DateOnly.FromDateTime(DateTime.UtcNow) <= c.AppraiseeDateRange.EndDate)
                .FirstOrDefault();

            if (inProgressCycle == null)
            {
                return Result<AppraisalCycleDTO>.NotFound("No in-progress cycle found.");
            }

            var cycleDto = new AppraisalCycleDTO
            {
                Id = inProgressCycle.Id,
                Quarter = inProgressCycle.Quarter.Name,
                Year = inProgressCycle.Year,
                AppraiseeStartDate = inProgressCycle.AppraiseeDateRange.StartDate,
                AppraiseeEndDate = inProgressCycle.AppraiseeDateRange.EndDate,
                AppraiserStartDate = inProgressCycle.AppraiserDateRange.StartDate,
                AppraiserEndDate = inProgressCycle.AppraiserDateRange.EndDate,
                Status = inProgressCycle.Status.Name,
            };

            return Result<AppraisalCycleDTO>.Success(cycleDto);
        }
        catch (Exception ex)
        {
            return Result<AppraisalCycleDTO>.Unavailable(ex.Message);
        }
    }
}
