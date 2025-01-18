using Bytestrone.AppraisalSystem.UseCases.AppraisalFeedbacks;

namespace Bytestrone.AppraisalSystem.Web.AppraisalFeedbacks;

public class GetAppraisalHistoryResponse
{
    public List<FeedbackHistoryDTO>? FeedbackHistory { get; set; }
    public string? Status { get; set; }
    public string? ErrorMessage { get; set; }
}
