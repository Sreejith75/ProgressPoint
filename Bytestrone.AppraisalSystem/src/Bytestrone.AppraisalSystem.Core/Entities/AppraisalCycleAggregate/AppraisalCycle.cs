using Ardalis.GuardClauses;
using Ardalis.SharedKernel;

namespace Bytestrone.AppraisalSystem.Core.Entities.AppraisalCycleAggregate;
public class AppraisalCycle : EntityBase, IAggregateRoot
{
    public Quarter Quarter { get; private set; } = default!;
    public int Year { get; private set; }
    public DateRange AppraiseeDateRange { get; private set; } = default!;
    public DateRange AppraiserDateRange { get; private set; } = default!;
    public CycleStatus Status { get; private set; } = default!;

    private AppraisalCycle() { }

    public AppraisalCycle(Quarter? quarter, int year, DateRange appraiseeDateRange, DateRange appraiserDateRange)
    {
        Guard.Against.Null(quarter, nameof(quarter));
        Guard.Against.NegativeOrZero(year, nameof(year));
        Guard.Against.Null(appraiseeDateRange, nameof(appraiseeDateRange));
        Guard.Against.Null(appraiserDateRange, nameof(appraiserDateRange));

        Quarter = quarter;
        Year = year;
        AppraiseeDateRange = appraiseeDateRange;
        AppraiserDateRange = appraiserDateRange;
        Status = CycleStatus.NotStarted;
    }

    public void StartCycle()
    {
        if (Status != CycleStatus.NotStarted)
        {
            throw new InvalidOperationException("The cycle must be in the 'NotStarted' state to start.");
        }

        Status = CycleStatus.InProgress;
    }

    public void CompleteCycle()
    {
        if (Status != CycleStatus.InProgress)
        {
            throw new InvalidOperationException("The cycle must be in the 'InProgress' state to complete.");
        }

        Status = CycleStatus.Completed;
    }

    public void ExtendDateRange(DateOnly newAppraiseeEndDate, DateOnly newAppraiserEndDate)
    {
        if (newAppraiseeEndDate <= AppraiseeDateRange.StartDate || newAppraiserEndDate <= AppraiserDateRange.StartDate)
        {
            throw new InvalidOperationException("End date must be after the start date.");
        }

        AppraiseeDateRange.ExtendEndDate(newAppraiseeEndDate);
        AppraiserDateRange.ExtendEndDate(newAppraiserEndDate);
    }
}

