using Ardalis.GuardClauses;
using Ardalis.SharedKernel;
using Bytestrone.AppraisalSystem.Core.Entities.PerformanceFactorAggregate;
using Bytestrone.AppraisalSystem.Core.Entities.QuestionAggregate;

namespace Bytestrone.AppraisalSystem.Core.Entities.PerformanceIndicatorAggregate;

public class PerformanceIndicator : EntityBase, IAggregateRoot
{
    // Properties
    public string? IndicatorName { get; private set; }
    public int FactorId { get; private set; }
    public decimal Weightage { get; private set; }
    public PerformanceFactor PerformanceFactor { get; private set; }=default!;

    private readonly List<Question> _questions = new List<Question>();
    public IReadOnlyCollection<Question> Questions => _questions.AsReadOnly();

    // Parameterized constructor
    public PerformanceIndicator(string indicatorName, int factorId, decimal weightage, PerformanceFactor performanceFactor)
    {
        // Constructor initialization
        IndicatorName = indicatorName;
        FactorId = factorId;
        Weightage = weightage;
        PerformanceFactor = Guard.Against.Null(performanceFactor, nameof(performanceFactor));
    }

    // Parameterless constructor for EF Core
    public PerformanceIndicator() { }
}
