namespace Bytestrone.AppraisalSystem.web.AppraisalFeedbacks;
public class UpdateAppraisalFeedbackDetailRequest
{
    public int EmployeeId { get; set; }
    public int FormId { get; set; }
    public int CycleId { get; set; }
    public List<AppraiseeFeedbackDetail>? FeedbackDetails { get; set; }
}
