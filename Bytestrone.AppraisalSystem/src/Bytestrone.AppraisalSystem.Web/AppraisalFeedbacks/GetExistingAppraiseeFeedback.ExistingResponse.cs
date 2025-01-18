namespace Bytestrone.AppraisalSystem.web.AppraisalFeedbacks;
public class ExistingFeedbackResponse
{
    public bool Status { get; set; }
    public int FeedbackId { get; set; }
    public int EmployeeId { get; set; }
    public int? AppraiserId { get; set; }
    public string? FeedbackStatus { get; set; }
    public IEnumerable<AppraiseeFeedbackDetail> FeedbackDetails { get; set; } = [];
}

