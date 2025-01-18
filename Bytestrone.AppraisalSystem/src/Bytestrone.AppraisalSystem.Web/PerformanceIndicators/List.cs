using Bytestrone.AppraisalSystem.UseCases.PerformanceFactors.ListIndicators;
using FastEndpoints;
using MediatR;

namespace Bytestrone.AppraisalSystem.web.PerformanceIndicators;
public class List(IMediator mediator): EndpointWithoutRequest<ListIndicatorsResponse>
{
  public override void Configure()
  {
    Get("/PerformanceIndicators");
    AllowAnonymous();
  }
  public override async Task HandleAsync(CancellationToken ct)
  {
    var query = new ListIndicatorsQuery();
    var result = await mediator.Send(query,ct);
    if(result.IsSuccess)
    {
        Response = new ListIndicatorsResponse
        {
            Message ="Success",
            factors=result.Value
        };
        return;
    }
    else
    {
        Response = new ListIndicatorsResponse
        {
            Message="Failed"
        };
        return;
    }
  }
}