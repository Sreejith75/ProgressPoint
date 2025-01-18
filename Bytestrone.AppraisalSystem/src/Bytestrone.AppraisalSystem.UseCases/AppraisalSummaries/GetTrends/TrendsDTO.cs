namespace Bytestrone.AppraisalSystem.UseCases.AppraisalSummaries.GetTrends;
public class TrendsDTO
{
    public int Year { get; set; }
    public string? Quarter { get; set; }
    public decimal AverageAppraiseeScore { get; set; }
    public decimal AverageAppraiserScore { get; set; }
}