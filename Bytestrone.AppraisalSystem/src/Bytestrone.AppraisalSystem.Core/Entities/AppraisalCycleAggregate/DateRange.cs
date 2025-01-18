using Ardalis.GuardClauses;
using Ardalis.SharedKernel;

namespace Bytestrone.AppraisalSystem.Core.Entities.AppraisalCycleAggregate;
public class DateRange : ValueObject
{
    public DateOnly StartDate { get; private set; }
    public DateOnly EndDate { get; private set; }

    // public DateRange() { }

    public DateRange(DateOnly startDate, DateOnly endDate)
    {
        Guard.Against.OutOfRange(startDate, nameof(startDate), DateOnly.MinValue, DateOnly.MaxValue);
        Guard.Against.OutOfRange(endDate, nameof(endDate), DateOnly.MinValue, DateOnly.MaxValue);

        if (startDate > endDate)
            throw new ArgumentException("Start date cannot be later than end date.");

        StartDate = startDate;
        EndDate = endDate;
    }

    public void ExtendEndDate(DateOnly newEndDate)
    {
        if (newEndDate <= EndDate)
            throw new InvalidOperationException("New end date must be later than the current end date.");

        EndDate = newEndDate;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return StartDate;
        yield return EndDate;
    }
}

