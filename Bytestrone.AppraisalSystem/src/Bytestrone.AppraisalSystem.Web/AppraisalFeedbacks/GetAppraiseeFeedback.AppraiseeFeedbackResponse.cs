using Bytestrone.AppraisalSystem.UseCases.AppraisalFeedbacks.GetAppraiseeFeedbacks;

namespace Bytestrone.AppraisalSystem.web.AppraisalFeedbacks;
public class AppraiseeFeedbackResponse
{
    public bool Status { get; set; }
    public string? ErrorMessage { get; set; }
    public int FeedbackId { get; set; }
    public string? FeedbackStatus { get; set; }
    public string? AppraiseeName { get; set; }
    public decimal AppraiseeScore { get; set; }
    public string? PerformanceBucket { get; set; }
    public List<PerformanceFactorsDTO>? Factors { get; set; }
}

