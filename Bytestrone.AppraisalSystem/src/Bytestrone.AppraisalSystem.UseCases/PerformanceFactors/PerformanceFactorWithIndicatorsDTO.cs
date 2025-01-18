using Bytestrone.AppraisalSystem.UseCases.PerformanceIndicators;

namespace Bytestrone.AppraisalSystem.UseCases.PerformanceFactors;

public record PerformanceFactorWithIndicatorsDTO
{
    public int Id { get; init; }
    public string? Name { get; init; }
    public List<PerformanceIndicatorsDTO>? Indicators { get; init; } = new();

    public PerformanceFactorWithIndicatorsDTO(int id, string? name, List<PerformanceIndicatorsDTO>? indicators)
    {
        Id = id;
        Name = name;
        Indicators = indicators;
    }
}
