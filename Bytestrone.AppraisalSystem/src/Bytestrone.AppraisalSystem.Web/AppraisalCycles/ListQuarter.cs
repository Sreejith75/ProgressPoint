using Bytestrone.AppraisalSystem.UseCases.AppraisalCycles.ListQuarter;
using FastEndpoints;
using MediatR;

namespace Bytestrone.AppraisalSystem.web.AppraisalCycles;
public class ListQuarter(IMediator mediator): EndpointWithoutRequest<ListQuarterResponse>
{
  public override void Configure()
  {
    Get("/appraisal-cycles/Quarter");
    AllowAnonymous();
  }
  public override async Task HandleAsync(CancellationToken ct)
  {
    var query = new ListQuarterQuery();
    var result = await mediator.Send(query, ct);
    if (result.IsSuccess)
    {
        Response = new ListQuarterResponse
        {
            Message="Success",
            Quarters=result.Value
        };
        return;
    }
    else
    {
        Response = new ListQuarterResponse
        {
            Message = "Failed"
        };
        return;
    }

  }
}