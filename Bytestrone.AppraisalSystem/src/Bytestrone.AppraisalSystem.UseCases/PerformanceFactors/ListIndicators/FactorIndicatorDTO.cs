namespace Bytestrone.AppraisalSystem.UseCases.PerformanceFactors.ListIndicators;
public class FactorDTO
{
    public int FactorId { get;set;}
    public string? FactorName { get;set;}
    public List<IndcatorDTO>? Indicators { get; set;}
}
public class IndcatorDTO
{
    public int IndicatorId { get; set;}
    public string? IndicatorName { get;set;}
    public decimal Weightage { get; set;}
}