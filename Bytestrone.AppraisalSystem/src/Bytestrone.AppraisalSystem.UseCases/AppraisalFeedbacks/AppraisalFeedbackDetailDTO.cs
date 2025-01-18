
namespace Bytestrone.AppraisalSystem.UseCases.AppraisalFeedbacks;
public record AppraisalFeedbackDetailDTO
{
    public int QuestionId { get; set; }
    public int AppraiseeRating { get; set; }
    public string? AppraiseeComment { get; set; }
    public byte[]? ArtifactData { get; set; }
    public string? ArtifactType { get; set; }

}