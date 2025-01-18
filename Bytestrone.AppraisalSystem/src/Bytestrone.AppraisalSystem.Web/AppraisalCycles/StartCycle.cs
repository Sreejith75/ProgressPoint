using Bytestrone.AppraisalSystem.UseCases.AppraisalCycles.UpdateStatus;
using FastEndpoints;
using MediatR;

namespace Bytestrone.AppraisalSystem.web.AppraisalCycles;
public class StartCycle(IMediator mediator) : EndpointWithoutRequest<StartCycleResponse>
{
  public override void Configure()
  {
    Put("/appraisal-cycles/UpdatStatus/{id}");
    AllowAnonymous();
  }
  public override async Task HandleAsync(CancellationToken ct)
  {
    var id = Route<int>("id");

    var result = await mediator.Send(new UpdateCycleStatusCommand(id), ct);
    if (result.IsSuccess)
    {
      Response = new StartCycleResponse(result.Value);
      return;
    }
    else
    {
      await SendErrorsAsync(result.Value, cancellation: ct);
    }
  }
}