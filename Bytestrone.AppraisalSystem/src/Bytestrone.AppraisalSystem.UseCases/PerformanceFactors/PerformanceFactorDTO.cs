using Bytestrone.AppraisalSystem.UseCases.PerformanceIndicators;

namespace Bytestrone.AppraisalSystem.UseCases.PerformanceFactors;

public record PerformanceFactorDTO
{
    public int Id { get; init; }
    public string? Name { get; init; }

    public PerformanceFactorDTO(int id, string? name)
    {
        Id = id;
        Name = name;
    }
}
