namespace Bytestrone.AppraisalSystem.UseCases.AppraisalFeedbacks;
public class AppraiserFeedbackDetailDTO
{
    public int QuestionId { get; set; }
    public int AppraiserRating { get; set; }
    public string? AppraiserComment { get; set; }
}