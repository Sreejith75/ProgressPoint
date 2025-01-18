using Ardalis.Result;
using Ardalis.SharedKernel;
using Bytestrone.AppraisalSystem.Core.Entities.AppraisalSummeryAggregate;
using Bytestrone.AppraisalSystem.Core.Entities.AppraisalSummeryAggregate.Specification;
using System.Linq;

namespace Bytestrone.AppraisalSystem.UseCases.AppraisalSummaries.GetTrends;
public class GetTrendsQueryHandler : IQueryHandler<GetTrendsQuery, Result<List<TrendsDTO>>>
{
    private readonly IRepository<AppraisalSummary> _appraisalSummeryRepository;

    public GetTrendsQueryHandler(IRepository<AppraisalSummary> appraisalSummeryRepository)
    {
        _appraisalSummeryRepository = appraisalSummeryRepository;
    }

    public async Task<Result<List<TrendsDTO>>> Handle(GetTrendsQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var appraisalSummerySpec = new ListSummerySpec();

            var appraisalSummaries = await _appraisalSummeryRepository.ListAsync(appraisalSummerySpec,cancellationToken);

            if (appraisalSummaries == null || !appraisalSummaries.Any())
            {
                return Result<List<TrendsDTO>>.Error("No appraisal summaries found.");
            }

            var trends = appraisalSummaries
                .GroupBy(a => new { a.AppraisalCycle.Year, a.AppraisalCycle.Quarter.Name })
                .Select(g => new TrendsDTO
                {
                    Year = g.Key.Year,
                    Quarter = g.Key.Name.ToString(),
                    AverageAppraiseeScore = g.Average(x => x.AppraiseeScore),
                    AverageAppraiserScore = g.Average(x => x.AppraiserScore)
                })
                .OrderBy(t => t.Year)
                .ThenBy(t => t.Quarter)
                .ToList();

            return Result<List<TrendsDTO>>.Success(trends);
        }
        catch (Exception ex)
        {
            return Result<List<TrendsDTO>>.Error($"Error fetching trends data: {ex.Message}");
        }
    }
}
