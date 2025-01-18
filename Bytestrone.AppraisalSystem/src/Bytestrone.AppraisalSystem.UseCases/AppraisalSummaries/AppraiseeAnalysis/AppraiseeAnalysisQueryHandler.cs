using Ardalis.Result;
using Ardalis.SharedKernel;
using Bytestrone.AppraisalSystem.Core.Entities.AppraisalSummeryAggregate;
using Bytestrone.AppraisalSystem.Core.Entities.AppraisalSummeryAggregate.Specification;
using System.Linq;

namespace Bytestrone.AppraisalSystem.UseCases.AppraisalSummaries.AppraiseeAnalysis;

public class AppraiseeAnalysisQueryHandler(IRepository<AppraisalSummary> appraisalSummeryRepository)
    : IQueryHandler<AppraiseeAnalysisQuery, Result<List<AppraiseeAnalysisDTO>>>
{
    private readonly IRepository<AppraisalSummary> _appraisalSummeryRepository = appraisalSummeryRepository;

    public async Task<Result<List<AppraiseeAnalysisDTO>>> Handle(AppraiseeAnalysisQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var appraisalSummerySpec = new ListSummeryByFilterSpec(request.RoleId, request.DepartmentId);
            var appraisalSummaries = await _appraisalSummeryRepository.ListAsync(appraisalSummerySpec, cancellationToken);

            var filteredSummaries = appraisalSummaries
                .Where(summary => summary.AppraisalCycle.Quarter.Value == request.QuarterId || summary.AppraisalCycle.Year == request.Year)
                .ToList();

            if (!filteredSummaries.Any())
            {
                return Result<List<AppraiseeAnalysisDTO>>.Error("No appraisee analysis data found for the specified filters.");
            }

            var appraiseeAnalysisList = filteredSummaries.Select(summary => new AppraiseeAnalysisDTO
            {
                AppraiseeId = summary.Employee.Id,
                AppraiseeName = summary.Employee.GetFullName(),
                AppraisalSummary = new AppraisalSummaryDTO
                {
                    SummaryId = summary.Id,
                    AppraiseeScore = summary.AppraiseeScore,
                    AppraiserScore = summary.AppraiserScore,
                    PerformanceBucket = summary.PerformanceBucket.Name
                }
            }).ToList();

            return Result<List<AppraiseeAnalysisDTO>>.Success(appraiseeAnalysisList);
        }
        catch (Exception ex)
        {
            return Result<List<AppraiseeAnalysisDTO>>.Error($"An error occurred while fetching appraisee analysis data: {ex.Message}");
        }
    }
}
