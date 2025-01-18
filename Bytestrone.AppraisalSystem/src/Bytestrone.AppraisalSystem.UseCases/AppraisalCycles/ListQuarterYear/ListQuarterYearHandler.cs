using Ardalis.Result;
using Ardalis.SharedKernel;
using Bytestrone.AppraisalSystem.Core.Entities.AppraisalCycleAggregate;
using System.Linq;

namespace Bytestrone.AppraisalSystem.UseCases.AppraisalCycles.ListQuarterYear;
public class ListQuarterHandler(IRepository<AppraisalCycle> appraisalCycleRepository) : IQueryHandler<ListQuarterYearQuery, Result<QuarterYearDTO>>
{
    private readonly IRepository<AppraisalCycle> _appraisalCycleRepository = appraisalCycleRepository;

    public async Task<Result<QuarterYearDTO>> Handle(ListQuarterYearQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var appraisalCycles = await _appraisalCycleRepository.ListAsync(cancellationToken);

            if (appraisalCycles == null || !appraisalCycles.Any())
            {
                return Result<QuarterYearDTO>.Error("No quarter-year data found.");
            }

            var uniqueQuarters = appraisalCycles
                .Select(ac => new QuarterDTO
                {
                    QuarterId = ac.Quarter.Value,
                    QuarterName = ac.Quarter.Name
                })
                .Distinct()
                .ToList();

            var uniqueYears = appraisalCycles
                .Select(ac => ac.Year)
                .Distinct()
                .ToList();

            var quarterYearDtos = new QuarterYearDTO
            {
                Quarter = uniqueQuarters,
                Year = uniqueYears
            };

            return Result<QuarterYearDTO>.Success(quarterYearDtos);
        }
        catch (Exception ex)
        {
            return Result<QuarterYearDTO>.Error($"Error fetching quarter-year data: {ex.Message}");
        }
    }
}