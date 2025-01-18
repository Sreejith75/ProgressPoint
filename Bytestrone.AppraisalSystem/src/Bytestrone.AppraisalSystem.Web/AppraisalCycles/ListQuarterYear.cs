using Bytestrone.AppraisalSystem.UseCases.AppraisalCycles.ListQuarterYear;
using FastEndpoints;
using MediatR;

namespace Bytestrone.AppraisalSystem.web.AppraisalCycles;

public class GetQuarterYearEndpoint(IMediator mediator) : EndpointWithoutRequest<ListQuarterYearResponse>
{
    private readonly IMediator _mediator = mediator;

    public override void Configure()
    {
        Get("/quarters-years");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var result = await _mediator.Send(new ListQuarterYearQuery(), ct);

        if (result.IsSuccess)
        {
            Response = new ListQuarterYearResponse
            {
                Message = "Success",
                quarterYears = result.Value
            };
            return;
        }
        else
        {
            Response = new ListQuarterYearResponse
            {
                Message = "Failed"
            };
            return;
        }
    }
}
