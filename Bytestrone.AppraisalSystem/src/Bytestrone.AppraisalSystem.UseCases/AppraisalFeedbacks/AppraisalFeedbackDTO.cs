namespace Bytestrone.AppraisalSystem.UseCases.AppraisalFeedbacks;
public record AppraisalFeedbackDTO
{
    public int FeedbackId { get; set; }
    public int EmployeeId { get; set; }
    public int? AppraiserId { get; set; }
    public string? Status { get; set; }
    public decimal? FinalScore { get; set; }
    public string? PerformanceBucketName { get; set; }
    public IEnumerable<AppraisalFeedbackDetailDTO> FeedbackDetails { get; set; } = [];
}