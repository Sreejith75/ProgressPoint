using Ardalis.Result;
using Bytestrone.AppraisalSystem.Core.Interfaces;
using Bytestrone.AppraisalSystem.Core.Entities.AppraisalSummeryAggregate;
using Bytestrone.AppraisalSystem.Core.Entities.AppraisalSummeryAggregate.Specification;
using Bytestrone.AppraisalSystem.Core.Entities.AppraisalCycleAggregate;
using System.Linq;
using Ardalis.SharedKernel;

namespace Bytestrone.AppraisalSystem.UseCases.AppraisalSummaries.GetBucketAnalysis;
public class GetBucketAnalysisQueryHandler : IQueryHandler<GetbucketAnalysisQuery, Result<BucketAnalysisResult>>
{
    private readonly IRepository<AppraisalSummary> _appraisalSummaryRepository;

    public GetBucketAnalysisQueryHandler(IRepository<AppraisalSummary> appraisalSummaryRepository)
    {
        _appraisalSummaryRepository = appraisalSummaryRepository;
    }

    public async Task<Result<BucketAnalysisResult>> Handle(GetbucketAnalysisQuery request, CancellationToken cancellationToken)
    {
        try
        {
            // Get all appraisal summaries without filtering by year and quarter
            var summarySpec = new ListSummerySpec();
            var appraisalSummaries = await _appraisalSummaryRepository.ListAsync(summarySpec, cancellationToken);

            if (appraisalSummaries == null || !appraisalSummaries.Any())
            {
                return Result.Error("No appraisal summaries found.");
            }

            // Calculate the total count of appraisal summaries before applying filters
            var totalSummariesCount = appraisalSummaries.Count;

            var quarterName = Quarter.FromValue(request.quarterId)?.Name;

            // Apply filtering in the handler
            var filteredSummaries = appraisalSummaries
                .Where(x => x.AppraisalCycle.Year == request.yearId &&
                            x.AppraisalCycle.Quarter.Name == quarterName)
                .ToList();

            if (!filteredSummaries.Any())
            {
                return Result.Error("No appraisal summaries found for the given year and quarter.");
            }

            var totalEmployees = filteredSummaries.Count;

            var bucketCounts = filteredSummaries
                .GroupBy(x => x.PerformanceBucket)
                .Select(g => new
                {
                    BucketId = g.Key.Value,
                    BucketName = g.Key.Name,
                    EmployeeCount = g.Count(),
                    Percentage = (g.Count() / (double)totalEmployees) * 100
                })
                .ToList();

            var bucketAnalysis = bucketCounts.Select(bucket => new BucketAnalysisDTO
            {
                BucketId = bucket.BucketId,
                BucketName = bucket.BucketName,
                EmployeeCount = bucket.EmployeeCount,
                Percentage = Math.Round(bucket.Percentage, 2)
            }).ToList();

            // Include the total count in the result
            var result = new BucketAnalysisResult
            {
                TotalSummariesCount = totalSummariesCount,
                BucketAnalysis = bucketAnalysis
            };

            return Result.Success(result);
        }
        catch (Exception ex)
        {
            return Result.Error($"Error fetching bucket analysis: {ex.Message}");
        }
    }
}