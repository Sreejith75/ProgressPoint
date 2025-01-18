namespace Bytestrone.AppraisalSystem.web.AppraisalCycles;

public record SimplifiedAppraisalCycleRecord
{
    public int Id { get; set; }
    public required string Quarter { get; set; }
    public int Year { get; set; }
}