using System.Net;
using Bytestrone.AppraisalSystem.UseCases.AppraisalCycles.GetCycleDetails;
using FastEndpoints;
using MediatR;

namespace Bytestrone.AppraisalSystem.web.AppraisalCycles;
public class GetCycleDetails(IMediator mediator) : EndpointWithoutRequest<CycleDetailsResponse>
{
    public override void Configure()
    {
        Get("/Appraisal-Cycles/Details");
        AllowAnonymous();
    }
    public override async Task HandleAsync(CancellationToken ct)
    {
        var query = new CycleDetailsQuery();
        var result = await mediator.Send(query, ct);
        if (result.IsSuccess)
        {
            Response = new CycleDetailsResponse
            {
                Message = "Success",
                cycleDetails = result.Value
            };
            return;
        }
        else
        {
            Response = new CycleDetailsResponse
            {
                Message = "Failed",
            };
            return;
        }
    }
}