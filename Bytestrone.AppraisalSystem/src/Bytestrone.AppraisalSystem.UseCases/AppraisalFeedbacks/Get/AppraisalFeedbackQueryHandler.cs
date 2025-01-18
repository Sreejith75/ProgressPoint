using Ardalis.Result;
using Ardalis.SharedKernel;
using Bytestrone.AppraisalSystem.Core.Entities.AppraisalFeedbackAggregate;
using Bytestrone.AppraisalSystem.Core.Entities.AppraisalFeedbackAggregate.Specification;
using Bytestrone.AppraisalSystem.Core.Entities.AppraisalSummeryAggregate;

namespace Bytestrone.AppraisalSystem.UseCases.AppraisalFeedbacks.Get;

public class AppraisalFeedbackQueryHandler(IRepository<AppraisalFeedback> appraisalFeedbackRepository) : IQueryHandler<AppraisalFeedbackQuery, Result<AppraisalFeedbackDTO>>
{
    private readonly IRepository<AppraisalFeedback> _appraisalFeedbackRepository = appraisalFeedbackRepository;

    public async Task<Result<AppraisalFeedbackDTO>> Handle(AppraisalFeedbackQuery request, CancellationToken cancellationToken)
    {
        var appraisalFeedback = await _appraisalFeedbackRepository.FirstOrDefaultAsync(
            new AppraisalFeedbackByEmployeeIdAndCycleIdSpec(request.EmployeeId, request.CycleId),
            cancellationToken
        );

        var FinalScore = appraisalFeedback?.AppraisalSummary?.AppraiseeScore;

        var PerformanceBucketName = FinalScore.HasValue 
    ? PerformanceBucket.GetBucketForScore(FinalScore.Value).Name 
    : "NotSet";

        if (appraisalFeedback == null)
        {
            return Result<AppraisalFeedbackDTO>.NotFound($"No appraisal feedback found for EmployeeId {request.EmployeeId} and CycleId {request.CycleId}.");
        }


        var appraisalFeedbackDto = new AppraisalFeedbackDTO
        {
            FeedbackId = appraisalFeedback.Id,
            EmployeeId = appraisalFeedback.EmployeeId,
            Status = appraisalFeedback.Status.Name,
            FeedbackDetails = appraisalFeedback.FeedbackDetails.Where(f => f.Status == FeedbackStatus.Pending).Select(f => new AppraisalFeedbackDetailDTO
            {
                QuestionId = f.QuestionId,
                AppraiseeRating = f.AppraiseeRating,
                AppraiseeComment = f.AppraiseeComment,

            }).ToList()
        };

        return Result<AppraisalFeedbackDTO>.Success(appraisalFeedbackDto);
    }
}
