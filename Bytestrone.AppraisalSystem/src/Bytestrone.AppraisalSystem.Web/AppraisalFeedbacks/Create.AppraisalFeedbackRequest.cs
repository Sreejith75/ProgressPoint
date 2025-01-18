using Microsoft.Identity.Client;

namespace Bytestrone.AppraisalSystem.web.AppraisalFeedbacks;
public class AppraisalFeedbackRequest
{
    // public const string Route = "/appraisal-feedback/{FormId:int}/{EmployeeId:int}";
    public int CycleId { get; set; }
    public int EmployeeId { get; set; } 
}