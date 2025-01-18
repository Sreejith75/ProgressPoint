using Bytestrone.AppraisalSystem.UseCases.AppraisalFeedbacks;
using Bytestrone.AppraisalSystem.UseCases.AppraisalFeedbacks.UpdateAppraiseeFeedback;
using FastEndpoints;
using MediatR;

namespace Bytestrone.AppraisalSystem.web.AppraisalFeedbacks;
public class UpdateAppraisalFeedbackEndpoint(IMediator mediator) : Endpoint<UpdateAppraisalFeedbackDetailRequest, UpdateAppraisalFeedbackDetailResponse>
{
    private readonly IMediator _mediator = mediator;

    public override void Configure()
    {
        Put("/appraisal-feedback/update");
        AllowAnonymous();
    }

    public override async Task HandleAsync(UpdateAppraisalFeedbackDetailRequest req, CancellationToken ct)
    {
        if (req.FeedbackDetails == null || req.FeedbackDetails.Count == 0)
        {
            await SendAsync(new UpdateAppraisalFeedbackDetailResponse(0, 0.00m, "Notset", "Feedback details cannot be empty."), 400, ct);
            return;
        }

        var feedbackDetails = new List<AppraisalFeedbackDetailDTO>();

        foreach (var fd in req.FeedbackDetails)
        {
            byte[]? artifactData = null;
            string? artifactType = null;

            if (fd.Artifact?.Length > 0)
            {
                using var memoryStream = new MemoryStream();
                await fd.Artifact.CopyToAsync(memoryStream, ct);
                artifactData = memoryStream.ToArray();
                artifactType = fd.Artifact.ContentType;

                if (artifactData.Length > 5 * 1024 * 1024) // 5 MB limit
                {
                    await SendAsync(new UpdateAppraisalFeedbackDetailResponse(0, 0.00m, "Notset", "Artifact file size exceeds the limit."), 400, ct);
                    return;
                }
            }

            feedbackDetails.Add(new AppraisalFeedbackDetailDTO
            {
                QuestionId = fd.QuestionId,
                AppraiseeRating = fd.AppraiseeRating,
                AppraiseeComment = fd.AppraiseeComment,
                ArtifactData = artifactData,
                ArtifactType = artifactType,
            });
        }

        var command = new UpdateAppraiseeFeedbackCommand(
            req.EmployeeId,
            req.CycleId,
            feedbackDetails
        );

        var result = await _mediator.Send(command, ct);

        if (result.IsSuccess)
        {
            var response = new UpdateAppraisalFeedbackDetailResponse(
                result.Value.Id,
                result.Value.FinalScore,
                result.Value.PerformanceBucket!,
                "Feedback successfully updated"
            );

            await SendOkAsync(response, ct);
        }
        else
        {
            var errorDto = result.Value;
            var errorMessage = result.Errors?.FirstOrDefault() ?? "Failed to save feedback.";
            var response = new UpdateAppraisalFeedbackDetailResponse(
                errorDto?.Id ?? 0,
                errorDto?.FinalScore ?? 0.00m,
                errorDto?.PerformanceBucket ?? "Notset",
                errorMessage
            );

            await SendAsync(response, 400, ct);
        }
    }
}
