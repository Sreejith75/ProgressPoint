using Ardalis.SharedKernel;
using Bytestrone.AppraisalSystem.Core.Entities.QuestionAggregate;

namespace Bytestrone.AppraisalSystem.Core.Entities.AppraisalFeedbackAggregate;

public class AppraisalFeedbackDetail : EntityBase
{
    private AppraisalFeedbackDetail() { }

    public AppraisalFeedbackDetail(int questionId, int appraiseeRating, string appraiseeComment, Artifact? artifact = null)
    {
        if (appraiseeRating < 0)
            throw new ArgumentOutOfRangeException(nameof(appraiseeRating), "Rating must be a non-negative value.");

        QuestionId = questionId;
        AppraiseeRating = appraiseeRating;
        AppraiseeComment = appraiseeComment;
        Artifact = artifact;
    }

    public int FeedbackId { get; private set; }
    public AppraisalFeedback? AppraisalFeedback { get; private set; } = default!;
    public int QuestionId { get; private set; }
    public Question? Question { get; private set; }
    public int AppraiseeRating { get; private set; }
    public string? AppraiseeComment { get; private set; }
    public Artifact? Artifact { get; private set; }
    public int? AppraiserRating { get; private set; }
    public string? AppraiserComment { get; private set; }
    public FeedbackStatus Status { get; set; } = FeedbackStatus.Pending;

    public void UpdateAppraiseeFeedback(int rating, string comment)
    {
        if (rating < 0)
            throw new ArgumentOutOfRangeException(nameof(rating), "Rating must be a non-negative value.");

        AppraiseeRating = rating;
        AppraiseeComment = comment;
    }

    public void UpdateAppraiserFeedback(int rating, string? comment)
    {
        if (rating < 0)
            throw new ArgumentOutOfRangeException(nameof(rating), "Rating must be a non-negative value.");

        AppraiserRating = rating;
        AppraiserComment = comment;
    }

    public void UpdateStatus(FeedbackStatus status)
    {
        Status = status;
    }

    public void AddArtifact(Artifact artifact)
    {
        Artifact = artifact;
    }
}
