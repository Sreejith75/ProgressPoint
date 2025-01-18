namespace Bytestrone.AppraisalSystem.web.AppraisalSummaries;
public record GetAppraisalSummaryResponse
{

    public int SummaryId { get; set; }
    public decimal AppraiseeScore { get; set; }
    public decimal AppraiserScore { get; set; }
    public string? PerformanceBucket { get; set; }
    public string? Description { get; set; }
    public string? Message { get; set; }
}