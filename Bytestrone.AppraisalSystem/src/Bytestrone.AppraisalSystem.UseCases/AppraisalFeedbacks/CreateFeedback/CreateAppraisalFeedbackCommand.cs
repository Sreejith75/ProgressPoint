using Ardalis.Result;
using Ardalis.SharedKernel;

namespace Bytestrone.AppraisalSystem.UseCases.AppraisalFeedbacks.CreateFeedback;

public record CreateAppraisalFeedbackCommand : ICommand<Result<int>>
{
    public int EmployeeId { get; set; }
    public int CycleId { get; set; }

    public CreateAppraisalFeedbackCommand( int employeeId, int cycleId)
    {
        EmployeeId = employeeId;
        CycleId = cycleId;
    }
}
