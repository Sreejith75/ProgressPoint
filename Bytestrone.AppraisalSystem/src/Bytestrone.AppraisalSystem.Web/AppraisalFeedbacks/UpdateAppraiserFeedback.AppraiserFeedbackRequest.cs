using Bytestrone.AppraisalSystem.UseCases.AppraisalFeedbacks;

namespace Bytestrone.AppraisalSystem.web.AppraisalFeedbacks;
public class AppraiserFeedbackRequest
{
    public int FeedbackId { get; set; }
    public int AppraiserId { get; set; }
    public List<AppraiserFeedbackDetailDTO>? appraiserFeedbackDetails { get; set; }
}