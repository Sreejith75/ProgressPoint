namespace Bytestrone.AppraisalSystem.UseCases.AppraisalFeedbacks;
public class AppraisalFeedbackResponseDTO
{
    public int Id { get; set; } 
    public decimal FinalScore { get; set; }
    public string? PerformanceBucket { get; set; } 
}