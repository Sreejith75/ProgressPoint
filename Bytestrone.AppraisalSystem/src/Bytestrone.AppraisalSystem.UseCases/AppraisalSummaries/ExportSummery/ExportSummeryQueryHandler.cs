using Ardalis.Result;
using Ardalis.SharedKernel;
using Bytestrone.AppraisalSystem.Core.Entities.AppraisalSummeryAggregate;
using Bytestrone.AppraisalSystem.Core.Entities.AppraisalSummeryAggregate.Specification;
using Bytestrone.AppraisalSystem.UseCases.Interface;
using System.Text;

namespace Bytestrone.AppraisalSystem.UseCases.AppraisalSummaries.ExportSummery;

public class ExportSummaryQueryHandler : IQueryHandler<ExportSummeryQuery, Result<byte[]>>
{
    private readonly IRepository<AppraisalSummary> _appraisalSummeryRepository;
    private readonly ICsvExportService _csvExportService;

    public ExportSummaryQueryHandler(IRepository<AppraisalSummary> appraisalSummeryRepository, ICsvExportService csvExportService)
    {
        _appraisalSummeryRepository = appraisalSummeryRepository;
        _csvExportService = csvExportService;
    }

    public async Task<Result<byte[]>> Handle(ExportSummeryQuery request, CancellationToken cancellationToken)
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
                return Result<byte[]>.Error("No appraisee analysis data found for the specified filters.");
            }

            var appraiseeAnalysisData = filteredSummaries.Select(summary => new AppraiseeAnalysisExportDTO
            {
                AppraiseeId = summary.Employee.Id,
                AppraiseeName = summary.Employee.GetFullName(),
                SummaryId = summary.Id,
                AppraiseeScore = summary.AppraiseeScore,
                AppraiserScore = summary.AppraiserScore,
                PerformanceBucket = summary.PerformanceBucket.Name
            }).ToList();

            var csvBytes = _csvExportService.GenerateCsv(appraiseeAnalysisData);
            return Result<byte[]>.Success(csvBytes); 
        }
        catch (Exception ex)
        {
            return Result<byte[]>.Error($"An error occurred while fetching appraisee analysis data: {ex.Message}");
        }
    }
}
