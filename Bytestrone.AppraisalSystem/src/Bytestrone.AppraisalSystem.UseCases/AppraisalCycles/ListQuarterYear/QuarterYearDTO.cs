using System.Security.Principal;

namespace Bytestrone.AppraisalSystem.UseCases.AppraisalCycles.ListQuarterYear;
public record QuarterYearDTO
{
    public List<QuarterDTO>? Quarter { get; set; }
    public List<int>? Year { get; set; }
 };

 public record QuarterDTO
 {
    public int QuarterId { get; set; }
    public string? QuarterName { get; set;}
 }