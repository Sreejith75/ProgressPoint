namespace Bytestrone.AppraisalSystem.UseCases.AppraisalSummaries;
public record AppraisalSummaryDTO
{
    public int SummaryId { get; set; }
    public decimal AppraiseeScore { get; set; }
    public decimal AppraiserScore { get; set; }
    public string? PerformanceBucket { get; set; }
    public string? Description { get; set; }
}