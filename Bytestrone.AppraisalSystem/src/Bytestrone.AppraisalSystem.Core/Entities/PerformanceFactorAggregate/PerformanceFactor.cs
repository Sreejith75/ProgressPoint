using Ardalis.GuardClauses;
using Ardalis.SharedKernel;
using Bytestrone.AppraisalSystem.Core.Entities.PerformanceIndicatorAggregate;

namespace Bytestrone.AppraisalSystem.Core.Entities.PerformanceFactorAggregate;
public class PerformanceFactor : EntityBase, IAggregateRoot
{
    public string FactorName { get; private set; }

    private readonly List<PerformanceIndicator> _indicators = new List<PerformanceIndicator>();
    public IReadOnlyCollection<PerformanceIndicator> Indicators => _indicators.AsReadOnly();

    private readonly List<DepartmentPerformanceFactor> _departmentPerformanceFactors = new List<DepartmentPerformanceFactor>();
    public IReadOnlyCollection<DepartmentPerformanceFactor> DepartmentPerformanceFactors => _departmentPerformanceFactors.AsReadOnly();  // Exposed to EF Core

    public PerformanceFactor(string factorName)
    {
        Guard.Against.NullOrEmpty(factorName, nameof(factorName));

        FactorName = factorName;
    }

    // public void AddIndicator(PerformanceIndicator indicator)
    // {
    //     Guard.Against.Null(indicator, nameof(indicator));
    //     _indicators.Add(indicator);
    // }

    // public void RemoveIndicator(PerformanceIndicator indicator)
    // {
    //     Guard.Against.Null(indicator, nameof(indicator)); 
    //     _indicators.Remove(indicator);
    // }
}
