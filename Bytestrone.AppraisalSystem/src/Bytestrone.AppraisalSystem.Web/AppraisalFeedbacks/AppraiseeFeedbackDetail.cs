using Microsoft.AspNetCore.Http;
namespace Bytestrone.AppraisalSystem.web.AppraisalFeedbacks;

public record AppraiseeFeedbackDetail
{
    public int QuestionId { get; set; }
    public int AppraiseeRating { get; set; }
    public string? AppraiseeComment { get; set; }
    public IFormFile? Artifact { get; set; }
}