namespace Bytestrone.AppraisalSystem.UseCases.AppraisalCycles.GetCycleDetails;
public class CycleDetailsDTO
{
    public int TotalEmployeeCount { get; set; }
    public int CompletedEmployeeCount { get; set; }
    public int PendingEmployeeCount { get; set; }
    public int UnderReviewEmployeeCount { get; set; }
    public int NotStartedEmployeeCount { get; set; }
}