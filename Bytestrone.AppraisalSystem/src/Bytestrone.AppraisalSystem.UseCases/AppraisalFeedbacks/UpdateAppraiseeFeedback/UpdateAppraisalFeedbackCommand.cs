using Ardalis.Result;
using Ardalis.SharedKernel;

namespace Bytestrone.AppraisalSystem.UseCases.AppraisalFeedbacks.UpdateAppraiseeFeedback;

public record UpdateAppraiseeFeedbackCommand : ICommand<Result<AppraisalFeedbackResponseDTO>>
{
    public int EmployeeId { get; set; }
    public int CycleId { get; set; }
    public List<AppraisalFeedbackDetailDTO> FeedbackDetails { get; set; } = new();

    public UpdateAppraiseeFeedbackCommand(int employeeId, int cycleId, List<AppraisalFeedbackDetailDTO> feedbackDetails)
    {
        EmployeeId = employeeId;
        CycleId = cycleId;
        FeedbackDetails = feedbackDetails ?? [];
    }
}
