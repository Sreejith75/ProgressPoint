
namespace Bytestrone.AppraisalSystem.web.AppraisalFeedbacks;
public record AppraisalFeedbackDetailRecord
{
    public int QuestionId { get; set; }
    public int AppraiseeRating { get; set; } 
    public string? AppraiseeComment { get; set; } 
    public byte[]? ArtifactData { get; set; }
    public string? ArtifactType { get; set; }

}