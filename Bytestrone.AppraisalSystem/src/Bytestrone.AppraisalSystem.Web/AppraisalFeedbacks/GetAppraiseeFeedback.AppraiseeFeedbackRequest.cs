using Google.Apis.Download;
using MediatR;

namespace Bytestrone.AppraisalSystem.web.AppraisalFeedbacks;
public class AppraiseeFeedbackRequest
{

    public int EmployeeId { get; set; }
    public int CycleId { get; set; }
}