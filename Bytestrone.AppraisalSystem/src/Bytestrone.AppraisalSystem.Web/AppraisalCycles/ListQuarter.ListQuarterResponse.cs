using Bytestrone.AppraisalSystem.UseCases.AppraisalCycles.ListQuarter;

namespace Bytestrone.AppraisalSystem.web.AppraisalCycles;
public class ListQuarterResponse
{
    public string? Message { get; set; }
    public List<QuarterDTO>? Quarters { get; set; }
}