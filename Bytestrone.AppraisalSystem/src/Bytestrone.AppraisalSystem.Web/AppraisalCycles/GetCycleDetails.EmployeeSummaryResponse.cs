using Bytestrone.AppraisalSystem.UseCases.AppraisalCycles.GetCycleDetails;

namespace Bytestrone.AppraisalSystem.web.AppraisalCycles;
public class CycleDetailsResponse
{
    public string? Message { get; set; }
    public CycleDetailsDTO? cycleDetails { get; set; }
}