using Bytestrone.AppraisalSystem.UseCases.AppraisalSummaries.GetBucketAnalysis;

namespace Bytestrone.AppraisalSystem.web.AppraisalSummaries;
public class BucketAnalysisResponse
{
    public string? Message { get; set; }
    public int TotalFeedbackCount { get; set; }
    public List<BucketAnalysisDTO>? bucketAnalysisDetails { get; set; }
}