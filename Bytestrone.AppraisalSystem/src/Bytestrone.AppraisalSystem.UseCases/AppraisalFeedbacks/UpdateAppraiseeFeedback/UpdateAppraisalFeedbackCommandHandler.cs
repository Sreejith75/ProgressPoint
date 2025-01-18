using Ardalis.GuardClauses;
using Ardalis.Result;
using Ardalis.SharedKernel;
using Bytestrone.AppraisalSystem.Core.Entities.AppraisalFeedbackAggregate;
using Bytestrone.AppraisalSystem.Core.Entities.AppraisalFeedbackAggregate.Specification;
using Bytestrone.AppraisalSystem.Core.Entities.AppraisalSummeryAggregate;
using Bytestrone.AppraisalSystem.Core.Entities.AppraisalSummeryAggregate.Specification;
using Bytestrone.AppraisalSystem.Core.Interfaces;
using Microsoft.Extensions.Logging;

namespace Bytestrone.AppraisalSystem.UseCases.AppraisalFeedbacks.UpdateAppraiseeFeedback;

public class UpdateAppraisalFeedbackCommandHandler(
    IRepository<AppraisalFeedback> appraisalFeedbackRepository,
    IRepository<AppraisalSummary> appraisalSummaryRepository,
    IArtifactStorageService artifactStorageService,
    ILogger<UpdateAppraisalFeedbackCommandHandler> logger) : ICommandHandler<UpdateAppraiseeFeedbackCommand, Result<AppraisalFeedbackResponseDTO>>
{
    private readonly IRepository<AppraisalFeedback> _appraisalFeedbackRepository = appraisalFeedbackRepository;
    private readonly IRepository<AppraisalSummary> _appraisalSummaryRepository = appraisalSummaryRepository;
    private readonly IArtifactStorageService _artifactStorageService = artifactStorageService;
    private readonly ILogger<UpdateAppraisalFeedbackCommandHandler> _logger = logger;

    public async Task<Result<AppraisalFeedbackResponseDTO>> Handle(UpdateAppraiseeFeedbackCommand request, CancellationToken cancellationToken)
    {
        try
        {
            Guard.Against.NullOrEmpty(request.FeedbackDetails, nameof(request.FeedbackDetails), "Feedback details cannot be empty.");

            var appraisalFeedbackSpec = new AppraisalFeedbackByEmployeeIdAndCycleIdSpec(request.EmployeeId, request.CycleId);
            var appraisalFeedback = await _appraisalFeedbackRepository.FirstOrDefaultAsync(appraisalFeedbackSpec, cancellationToken)
                ?? throw new Exception($"Appraisal feedback not found");

            var requestQuestionIds = new HashSet<int>(request.FeedbackDetails.Select(fd => fd.QuestionId));

            foreach (var detail in request.FeedbackDetails)
            {
                var existingFeedbackDetail = appraisalFeedback.FeedbackDetails
                    .FirstOrDefault(fd => fd.QuestionId == detail.QuestionId);

                if (existingFeedbackDetail != null)
                {
                    existingFeedbackDetail.UpdateAppraiseeFeedback(detail.AppraiseeRating, detail.AppraiseeComment ?? "No Comments");
                    existingFeedbackDetail.UpdateStatus(FeedbackStatus.Pending);
                }
                else
                {
                    var feedbackDetail = new AppraisalFeedbackDetail(
                        detail.QuestionId,
                        detail.AppraiseeRating,
                        detail.AppraiseeComment ?? "No Comments"
                    );
                    feedbackDetail.UpdateStatus(FeedbackStatus.Pending);

                    if (detail.ArtifactData != null && detail.ArtifactType != null && detail.ArtifactData.Length > 0)
                    {
                        try
                        {
                            var savedArtifact = await _artifactStorageService.SaveArtifactAsync(detail.ArtifactData, detail.ArtifactType);
                            feedbackDetail.AddArtifact(new Artifact(savedArtifact.FilePath, savedArtifact.FileType));
                        }
                        catch (Exception ex)
                        {
                            _logger.LogError(ex, "Error saving artifact for feedback detail {DetailId}", detail.QuestionId);
                            return Result.Error("Error saving artifact. Please try again.");
                        }
                    }

                    appraisalFeedback.AddFeedbackDetail(feedbackDetail);
                }
            }

            await _appraisalFeedbackRepository.UpdateAsync(appraisalFeedback, cancellationToken);

            var latestFeedbackSpec = new AppraisalFeedbackByIdSpec(appraisalFeedback.Id);
            var latestFeedback = await _appraisalFeedbackRepository.FirstOrDefaultAsync(latestFeedbackSpec, cancellationToken);

            if (latestFeedback == null)
            {
                return Result.Error("Failed to retrieve updated appraisal feedback.");
            }

            var totalAppraiseeScore = latestFeedback.CalculateAppraiseeScore();

            var summarySpecForCreation = new GetSummeryByEmployeeIdCycleIdSpec(latestFeedback.EmployeeId, latestFeedback.CycleId);
            var summaryForCreation = await _appraisalSummaryRepository.FirstOrDefaultAsync(summarySpecForCreation, cancellationToken);

            if (summaryForCreation == null)
            {
                return Result.Error("No appraisal summary found for the given employee and cycle.");
            }

            summaryForCreation.UpdateAppraiseeScore(totalAppraiseeScore);
            await _appraisalSummaryRepository.UpdateAsync(summaryForCreation, cancellationToken);

            return Result.Success(new AppraisalFeedbackResponseDTO
            {
                Id = latestFeedback.Id,
                FinalScore = summaryForCreation.AppraiseeScore,
                PerformanceBucket = PerformanceBucket.GetBucketForScore(summaryForCreation.AppraiseeScore).Name
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating appraisal feedback for request {RequestId}", request.EmployeeId);
            return Result.Error($"Error updating appraisal feedback: {ex.Message}");
        }
    }


}
