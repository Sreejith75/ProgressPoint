using Bytestrone.AppraisalSystem.UseCases.AppraisalFeedbacks.UpdateFeedbackStatus;
using FastEndpoints;
using MediatR;

namespace Bytestrone.AppraisalSystem.web.AppraisalFeedbacks;
public class UpdateFeedbackStatus(IMediator mediator) : Endpoint<FeedbackStatusRequest, FeedbackStatusResponse>
{
  public override void Configure()
  {
    Put("/appraisal-feedback/UpdateStatus");
    AllowAnonymous();
  }
  public override async Task HandleAsync(FeedbackStatusRequest req, CancellationToken ct)
  {
    var command = new UpdateFeedbackStatusCommand(req.FeedbackId);
    var result = await mediator.Send(command, ct);
    if (result.IsSuccess)
    {
      Response = new FeedbackStatusResponse(result.Value, true, "Status Successfully Updated");
    }
    else
    {
      Response = new FeedbackStatusResponse(result.Value, false, "Faild to update status");
    }
  }
}