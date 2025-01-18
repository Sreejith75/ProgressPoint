namespace Bytestrone.AppraisalSystem.UseCases.AppraisalSummaries.GetBucketAnalysis;
public class BucketAnalysisDTO
{
    public int BucketId { get; set; }
    public string? BucketName { get; set; }
    public int EmployeeCount { get; set; }
    public double Percentage { get; set; }
}