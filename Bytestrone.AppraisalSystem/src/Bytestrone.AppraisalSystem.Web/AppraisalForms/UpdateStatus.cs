using Bytestrone.AppraisalSystem.UseCases.AppraisalForms.UpdateFormStatus;
using FastEndpoints;
using MediatR;

namespace Bytestrone.AppraisalSystem.web.AppraisalForms;
public class UpdateStatus(IMediator mediator) : EndpointWithoutRequest<UpdateFormStatusResponse>
{
  public override void Configure()
  {
    Put("/appraisal-forms/UpdateStatus/{id}");
    AllowAnonymous();
  }
  public override async Task HandleAsync(CancellationToken ct)
  {
    var id=Route<int>("id");
    var result = await mediator.Send(new UpdateFormStatusCommand(id), ct);
    if (result.IsSuccess)
    {
        Response = new UpdateFormStatusResponse(result.Value);
        return;
    }
    else
    {
      await SendErrorsAsync(result.Value, cancellation: ct);
    }

  }
}