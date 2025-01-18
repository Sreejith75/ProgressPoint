namespace Bytestrone.AppraisalSystem.web.AppraisalFeedbacks;
public class FeedbackStatusResponse(int FeedbackId,bool status, string message)
{
    public int FeedbackId { get; set; }=FeedbackId;
    public bool Status { get; set; }=status;
    public string? Message { get; set; }=message;
}