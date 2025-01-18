using Ardalis.Result;
using Ardalis.SharedKernel;
using Bytestrone.AppraisalSystem.Core.Entities.AppraisalFeedbackAggregate;
using Bytestrone.AppraisalSystem.Core.Entities.AppraisalFeedbackAggregate.Specification;
using Bytestrone.AppraisalSystem.Core.Entities.AppraisalSummeryAggregate;

namespace Bytestrone.AppraisalSystem.UseCases.AppraisalFeedbacks.GetHistory;

public class GetAppraisalHistoryQueryHandler(IRepository<AppraisalFeedback> appraisalFeedback)
    : IQueryHandler<GetAppraisalHistoryQuery, Result<List<FeedbackHistoryDTO>>>
{
    private readonly IRepository<AppraisalFeedback> _appraisalFeedbackRepository = appraisalFeedback;

    public async Task<Result<List<FeedbackHistoryDTO>>> Handle(GetAppraisalHistoryQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var feedbackSpec = new AppraisalFeedbackByEmployeeIdSpec(request.EmployeeId);
            var feedbacks = await _appraisalFeedbackRepository.ListAsync(feedbackSpec, cancellationToken);

            if (feedbacks == null || feedbacks.Count == 0)
            {
                return Result.NotFound($"No feedback history found");
            }

            var feedbackHistoryDtos = feedbacks.Select(fb => new FeedbackHistoryDTO
            {
                FeedbackId = fb.Id,
                Quarter = fb.AppraisalCycle?.Quarter.Name ?? "N/A",
                Year = fb.AppraisalCycle?.Year ?? 0,
                AppraiseeScore = fb.AppraisalSummary?.AppraiseeScore ?? 0.00m,
                AppraiserScore = fb.AppraisalSummary?.AppraiserScore ?? 0.00m,
                AssessmentStatus=fb.Status.Name,
                PerformanceBucketName = fb.AppraisalSummary?.AppraiserScore != 0.00m
                    ? PerformanceBucket.GetBucketForScore(fb.AppraisalSummary!.AppraiserScore).Name
                    : PerformanceBucket.GetBucketForScore(fb.AppraisalSummary?.AppraiseeScore ?? 0.0m).Name
            }).ToList();

            return Result.Success(feedbackHistoryDtos);
        }
        catch (Exception ex)
        {
            return Result.Error($"An error occurred while fetching feedback history: {ex.Message}");
        }
    }
}
