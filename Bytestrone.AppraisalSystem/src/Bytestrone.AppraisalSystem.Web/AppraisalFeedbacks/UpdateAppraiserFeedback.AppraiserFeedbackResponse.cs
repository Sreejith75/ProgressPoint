namespace Bytestrone.AppraisalSystem.web.AppraisalFeedbacks;
public class AppraiserFeedbackResponse
{
    public int Id { get; set; }
    public decimal FinalScore { get; set; } 
    public string? PerformanceBucket { get; set; }
    public string? Message { get; set; }
    
}