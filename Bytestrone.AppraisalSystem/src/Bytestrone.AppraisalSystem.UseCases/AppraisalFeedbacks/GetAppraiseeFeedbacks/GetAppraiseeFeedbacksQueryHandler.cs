using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Ardalis.Result;
using Ardalis.SharedKernel;
using Bytestrone.AppraisalSystem.Core.Entities.AppraisalFeedbackAggregate;
using Bytestrone.AppraisalSystem.Core.Entities.AppraisalFeedbackAggregate.Specification;

namespace Bytestrone.AppraisalSystem.UseCases.AppraisalFeedbacks.GetAppraiseeFeedbacks;

public class GetAppraiseeFeedbacksQueryHandler(IRepository<AppraisalFeedback> appraisalFeedbackRepository) : IQueryHandler<GetAppraiseeFeedbacksQuery, Result<AppraiseeFeedbackDetailDTO>>
{
    private readonly IRepository<AppraisalFeedback> _appraisalFeedbackRepository = appraisalFeedbackRepository;

    public async Task<Result<AppraiseeFeedbackDetailDTO>> Handle(GetAppraiseeFeedbacksQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var feedbackSpec = new AppraisalFeedbackByEmployeeIdAndCycleIdSpec(request.AppraiseeId, request.CycleId);
            var feedback = await _appraisalFeedbackRepository.FirstOrDefaultAsync(feedbackSpec, cancellationToken);

            if (feedback == null)
                return Result.NotFound("No feedback found for the given AppraiseeId and CycleId.");

            var feedbackDto = new AppraiseeFeedbackDetailDTO
            {
                feedbackId = feedback.Id,
                feedbackStatus = feedback.Status.Name,
                AppraiseeScore=feedback.AppraisalSummary!.AppraiseeScore,
                PerformanceBucket=feedback.AppraisalSummary.PerformanceBucket.Name,
                AppraiseeName = feedback.Employee?.GetFullName(),

                factors = feedback.FeedbackDetails
                    .GroupBy(fd => fd.Question!.Indicator!.PerformanceFactor)
                    .Select(factorGroup => new PerformanceFactorsDTO
                    {
                        FactorId = factorGroup.Key.Id,
                        FactorName = factorGroup.Key.FactorName,
                        Indicators = factorGroup
                            .GroupBy(fd => fd.Question!.Indicator)
                            .Select(indicatorGroup => new PerformanceIndicatorDTO
                            {
                                IndicatorId = indicatorGroup.Key.Id,
                                IndicatorName = indicatorGroup.Key.IndicatorName,
                                Question = indicatorGroup.Select(fd => new QuestionsDTO
                                {
                                    QuestId = fd.QuestionId,
                                    QuestionText = fd.Question?.QuestionText,
                                    AppraiseeRating = fd.AppraiseeRating,
                                    AppraiseeComment = fd.AppraiseeComment,
                                    AppraiserRating = fd.AppraiserRating,
                                    AppraiserComment = fd.AppraiserComment
                                }).ToList()
                            }).ToList()
                    }).ToList()
            };

            return Result.Success(feedbackDto);
        }
        catch (Exception ex)
        {
            return Result.Error($"An error occurred while processing the request: {ex.Message}");
        }
    }
}
