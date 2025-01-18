using Ardalis.GuardClauses;
using Ardalis.SharedKernel;
using Bytestrone.AppraisalSystem.Core.EmployeeAggregate;

namespace Bytestrone.AppraisalSystem.Core.Entities.EmployeeAggregate;

public class EmployeeAppraiserMapping(int employeeId, int appraiserId, DateTime effectiveDate) : EntityBase
{
    public int EmployeeId { get; private set; } = employeeId;
    public virtual Employee? Appraisee { get; private set; }

    public int AppraiserId { get; private set; } = appraiserId;
    public virtual Employee? Appraiser { get; private set; }

    public DateTime EffectiveDate { get; private set; } = effectiveDate;
    public DateTime? EndDate { get; private set; }
    public string Status { get; private set; } = "Active";
    public string? ChangedReason { get; private set; }


    public void TerminateRelationship(DateTime endDate, string reason)
    {
        Guard.Against.Default(endDate, nameof(endDate));
        Guard.Against.NullOrEmpty(reason, nameof(reason));

        EndDate = endDate;
        Status = "Inactive";
        ChangedReason = reason;
    }
    public void UpdateChangedReason(DateTime endDate, string reason)
    {
        EndDate= endDate;
        ChangedReason=reason;
    }
}
