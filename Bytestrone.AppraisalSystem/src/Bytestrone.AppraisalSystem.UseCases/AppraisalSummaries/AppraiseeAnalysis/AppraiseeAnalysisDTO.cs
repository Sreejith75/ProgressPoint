namespace Bytestrone.AppraisalSystem.UseCases.AppraisalSummaries.AppraiseeAnalysis;
public class AppraiseeAnalysisDTO
{
    public int AppraiseeId { get; set; }
    public string? AppraiseeName { get; set; }
    public AppraisalSummaryDTO? AppraisalSummary { get; set; }
}