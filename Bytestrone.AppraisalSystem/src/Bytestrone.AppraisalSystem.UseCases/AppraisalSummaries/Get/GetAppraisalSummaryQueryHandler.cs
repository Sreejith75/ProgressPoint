using Ardalis.Result;
using Ardalis.SharedKernel;
using Bytestrone.AppraisalSystem.Core.Entities.AppraisalSummeryAggregate;
using Bytestrone.AppraisalSystem.Core.Entities.AppraisalSummeryAggregate.Specification;

namespace Bytestrone.AppraisalSystem.UseCases.AppraisalSummaries.Get;

public class GetAppraisalSummeryQueryHandler(IRepository<AppraisalSummary> appraisalSummaryRepository) 
    : IQueryHandler<GetAppraisalSummeryQuery, Result<AppraisalSummaryDTO>>
{
    private readonly IRepository<AppraisalSummary> _appraisalSummaryRepository = appraisalSummaryRepository;

    public async Task<Result<AppraisalSummaryDTO>> Handle(GetAppraisalSummeryQuery request, CancellationToken cancellationToken)
    {
        var summarySpec = new GetSummeryByEmployeeIdCycleIdSpec(request.EmployeeId, request.CycleId);
        var summary = await _appraisalSummaryRepository.FirstOrDefaultAsync(summarySpec, cancellationToken);

        if (summary == null)
        {
            return Result.Error($"No appraisal summary found for EmployeeId: {request.EmployeeId}, CycleId: {request.CycleId}");
        }

        var summaryDto = new AppraisalSummaryDTO
        {
            SummaryId=summary.Id,
            AppraiseeScore = summary.AppraiseeScore,
            AppraiserScore = summary.AppraiserScore,
            PerformanceBucket = summary.PerformanceBucket.Name,
            Description= summary.PerformanceBucket.Description
        };

        // Return the successful result with the DTO
        return Result.Success(summaryDto);
    }
}
