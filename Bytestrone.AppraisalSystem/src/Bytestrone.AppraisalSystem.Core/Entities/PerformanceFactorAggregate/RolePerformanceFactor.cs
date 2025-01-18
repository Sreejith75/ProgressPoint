using Ardalis.SharedKernel;
using Bytestrone.AppraisalSystem.Core.Entities.DepartmentAggregate;

namespace Bytestrone.AppraisalSystem.Core.Entities.PerformanceFactorAggregate;

public class DepartmentPerformanceFactor : EntityBase
{
    public decimal Weightage { get; private set; }
    public int DepartmentId { get; private set; }
    public Department Department { get; private set; } = default!;
    public int PerformanceFactorId { get; private set; }
    public PerformanceFactor PerformanceFactor { get; private set; } = default!;

    private DepartmentPerformanceFactor() { } // For EF Core

    public DepartmentPerformanceFactor(decimal weightage, int departmentId, Department department, int performanceFactorId, PerformanceFactor performanceFactor)
    {
        if (weightage < 0 || weightage > 100)
            throw new ArgumentOutOfRangeException(nameof(weightage), "Weightage must be between 0 and 100.");

        Weightage = weightage;
        DepartmentId = departmentId;
        Department = department ?? throw new ArgumentNullException(nameof(department));
        PerformanceFactorId = performanceFactorId;
        PerformanceFactor = performanceFactor ?? throw new ArgumentNullException(nameof(performanceFactor));
    }

    public void UpdateWeightage(decimal weightage)
    {
        if (weightage < 0 || weightage > 100)
            throw new ArgumentOutOfRangeException(nameof(weightage), "Weightage must be between 0 and 100.");

        Weightage = weightage;
    }
}
