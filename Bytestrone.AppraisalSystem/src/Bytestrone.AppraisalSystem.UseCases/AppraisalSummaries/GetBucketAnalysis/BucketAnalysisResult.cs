namespace Bytestrone.AppraisalSystem.UseCases.AppraisalSummaries.GetBucketAnalysis;
public class BucketAnalysisResult
{
    public int TotalSummariesCount { get; set; }
    public List<BucketAnalysisDTO>? BucketAnalysis { get; set; }
}
