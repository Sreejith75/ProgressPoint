namespace Bytestrone.AppraisalSystem.UseCases.AppraisalSummaries.ExportSummery;
public class AppraiseeAnalysisExportDTO
{
    public int AppraiseeId { get; set; }
    public string? AppraiseeName { get; set; }
    public int SummaryId { get; set; }
    public decimal AppraiseeScore { get; set; }
    public decimal AppraiserScore { get; set; }
    public string? PerformanceBucket { get; set; }
}
