using Ardalis.SharedKernel;
using Bytestrone.AppraisalSystem.Core.EmployeeAggregate;
using Bytestrone.AppraisalSystem.Core.Entities.AppraisalCycleAggregate;
using Bytestrone.AppraisalSystem.Core.Entities.AppraisalFormAggregate;
using Bytestrone.AppraisalSystem.Core.Entities.AppraisalSummeryAggregate;

namespace Bytestrone.AppraisalSystem.Core.Entities.AppraisalFeedbackAggregate;

public partial class AppraisalFeedback( int cycleId, int employeeId) : EntityBase, IAggregateRoot
{
   
    public int CycleId { get; private set; } = cycleId;
    public AppraisalCycle AppraisalCycle { get; private set; } = default!;
    public int EmployeeId { get; private set; } = employeeId;
    public Employee? Employee { get; private set; }
    public int AppraiserId { get; set; }
    public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;
    public int? SummaryId { get; private set; }
    public AppraisalSummary? AppraisalSummary { get; private set; }
    public FeedbackStatus Status { get; set; } = FeedbackStatus.NotSet;

    private readonly List<AppraisalFeedbackDetail> _feedbackDetails = new();
    public IReadOnlyCollection<AppraisalFeedbackDetail> FeedbackDetails => _feedbackDetails.AsReadOnly();

    // Add feedback detail
    public void AddFeedbackDetail(AppraisalFeedbackDetail detail)
    {
        if (detail == null)
            throw new ArgumentNullException(nameof(detail), "Feedback detail cannot be null.");

        _feedbackDetails.Add(detail);
    }

    // Update the status of the feedback
    public void UpdateStatus(FeedbackStatus status)
    {
        Status = status;
    }

    // Create summary
    public void CreateSummary(int employeeId)
    {
        if (AppraisalSummary != null)
            throw new InvalidOperationException("Summary has already been created.");

        AppraisalSummary = new AppraisalSummary(employeeId,CycleId);
    }
}
