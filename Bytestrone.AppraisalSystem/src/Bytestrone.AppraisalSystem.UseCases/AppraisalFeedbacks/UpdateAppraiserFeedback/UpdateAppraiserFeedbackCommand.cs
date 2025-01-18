using System.Windows.Input;
using Ardalis.Result;
using Ardalis.SharedKernel;

namespace Bytestrone.AppraisalSystem.UseCases.AppraisalFeedbacks.UpdateAppraiserFeedback;
public record UpdateAppraiserFeedbackCommand:ICommand<Result<AppraisalFeedbackResponseDTO>>
{
    public int FeedbackId { get; set; }
    public int ApprasierId { get; set; }
    public List<AppraiserFeedbackDetailDTO> AppraiserFeedbackDetails { get; set; }
    public UpdateAppraiserFeedbackCommand(int feedbackId,int appraiserId, List<AppraiserFeedbackDetailDTO> FeedbackDetails)
    {
        FeedbackId = feedbackId;
        ApprasierId=appraiserId;
        AppraiserFeedbackDetails = FeedbackDetails;
    }
}