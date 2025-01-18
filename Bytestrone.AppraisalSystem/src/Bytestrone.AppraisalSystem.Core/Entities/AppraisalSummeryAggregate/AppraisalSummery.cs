using System.Security.Cryptography;
using Ardalis.SharedKernel;
using Bytestrone.AppraisalSystem.Core.EmployeeAggregate;
using Bytestrone.AppraisalSystem.Core.Entities.AppraisalCycleAggregate;

namespace Bytestrone.AppraisalSystem.Core.Entities.AppraisalSummeryAggregate;

public class AppraisalSummary(int employeeId, int cycleId) : EntityBase, IAggregateRoot
{
    public int EmployeeId { get; private set; } = employeeId;
    public Employee Employee { get; private set; } = default!;

    public int CycleId { get; private set; } = cycleId;
    public AppraisalCycle AppraisalCycle { get; private set; } = default!;

    public decimal AppraiseeScore { get; private set; }
    public decimal AppraiserScore { get; private set; }
    public PerformanceBucket PerformanceBucket { get; private set; } = PerformanceBucket.NotSet;



    public void UpdateAppraiseeScore(decimal appraiseeScore)
    {
        if (appraiseeScore < 0)
            throw new ArgumentOutOfRangeException("Scores must be non-negative.");

        AppraiseeScore = appraiseeScore;
        PerformanceBucket= PerformanceBucket.GetBucketForScore(appraiseeScore);
    }
    public void UpdateAppraiserScore(decimal appraiserScore)
    {
        if (appraiserScore < 0)
            throw new ArgumentOutOfRangeException("Scores must be non-negative.");

        AppraiserScore = appraiserScore;
        PerformanceBucket= PerformanceBucket.GetBucketForScore(appraiserScore);
    }
    public void UpdatePerformanceBucket(PerformanceBucket bucket)
    {
        PerformanceBucket=bucket;
    }
}

