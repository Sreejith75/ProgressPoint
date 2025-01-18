namespace Bytestrone.AppraisalSystem.UseCases.AppraisalCycles;

public record ActiveCycleDTO
{
    public int Id { get; set; }
    public required string Quarter { get; set; }
    public int Year { get; set; }
}