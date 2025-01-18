namespace Bytestrone.AppraisalSystem.UseCases.PerformanceFactors;
public record PerformanceIndicatorsDTO
{
    public int IndicatorId { get; init; } 
    public string IndicatorName { get; init; } 
    public decimal Weightage { get; init; }

    public PerformanceIndicatorsDTO(int id, string name)
    {
        IndicatorId = id;
        IndicatorName = name;
    }
}