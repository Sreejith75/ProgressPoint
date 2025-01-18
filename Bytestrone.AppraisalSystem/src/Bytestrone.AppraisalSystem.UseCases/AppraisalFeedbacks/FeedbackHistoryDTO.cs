namespace Bytestrone.AppraisalSystem.UseCases.AppraisalFeedbacks;
public record FeedbackHistoryDTO
{
    public int FeedbackId { get; set; }
    public string? Quarter { get; set; }
    public int Year { get; set; }
    public decimal AppraiseeScore { get; set; }
    public decimal AppraiserScore { get; set; }
    public string? PerformanceBucketName { get; set; }
    public string? AssessmentStatus {get; set;} 
}