
namespace Bytestrone.AppraisalSystem.web.AppraisalFeedbacks;
public class UpdateAppraisalFeedbackDetailResponse(int Id, decimal FinalScore, string performanceBucket,string Message)
{
    public int Id { get; set; } = Id;
    public decimal FinalScore { get; set; } = FinalScore;
    public string PerformanceBucket { get; set; } = performanceBucket;
    public string Message { get; set; } = Message;
}