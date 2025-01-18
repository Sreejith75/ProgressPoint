using Ardalis.Result;
using Ardalis.SharedKernel;
using Bytestrone.AppraisalSystem.Core.Entities.AppraisalFeedbackAggregate;
using Bytestrone.AppraisalSystem.Core.Entities.AppraisalFeedbackAggregate.Specification;
using Bytestrone.AppraisalSystem.Core.Entities.AppraisalFormAggregate;
using Bytestrone.AppraisalSystem.Core.Entities.AppraisalFormAggregate.Specification;

namespace Bytestrone.AppraisalSystem.UseCases.AppraisalFeedbacks.CreateFeedback;

public class CreateAppraisalFeedbackCommandHandler(
    IRepository<AppraisalFeedback> appraisalFeedbackRepository, 
    IRepository<AppraisalForm> appraisalFormRepository) 
    : ICommandHandler<CreateAppraisalFeedbackCommand, Result<int>>
{
    private readonly IRepository<AppraisalFeedback> _appraisalFeedbackRepository = appraisalFeedbackRepository;
    private readonly IRepository<AppraisalForm> _appraisalFormRepository = appraisalFormRepository;

    public async Task<Result<int>> Handle(CreateAppraisalFeedbackCommand request, CancellationToken cancellationToken)
    {
        try
        {
            
                var appraisalFeedbackSpec = new AppraisalFeedbackByEmployeeIdAndCycleIdSpec(
                request.EmployeeId, 
                request.CycleId);

            var existingFeedback = await _appraisalFeedbackRepository
                .FirstOrDefaultAsync(appraisalFeedbackSpec, cancellationToken);

            if (existingFeedback != null)
            {
                return Result.Error("Feedback already exists for this employee and appraisal cycle.");
            }

            var newFeedback = new AppraisalFeedback(
                employeeId: request.EmployeeId,
                cycleId:request.CycleId
            );

            newFeedback.CreateSummary(request.EmployeeId);
            newFeedback.UpdateStatus(FeedbackStatus.Pending);

            await _appraisalFeedbackRepository.AddAsync(newFeedback, cancellationToken);

            return Result.Success(newFeedback.Id);
        }
        catch (Exception ex)
        {
            return Result.Error($"Error creating appraisal feedback: {ex.Message}");
        }
    }
}
