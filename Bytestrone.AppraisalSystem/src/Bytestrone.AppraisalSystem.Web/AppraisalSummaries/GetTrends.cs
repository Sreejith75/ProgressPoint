using Bytestrone.AppraisalSystem.UseCases.AppraisalSummaries.GetTrends;
using FastEndpoints;
using MediatR;

namespace Bytestrone.AppraisalSystem.web.AppraisalSummaries;
public class GetTrends(IMediator mediator) : EndpointWithoutRequest<TrendsResponse>
{
    private readonly IMediator _mediator = mediator;

  public override void Configure()
    {
        Get("/AppraisalSummary/trends");
        AllowAnonymous(); 
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        // Create the query object
        var query = new GetTrendsQuery();

        // Send the query to the mediator to get the result
        var result = await _mediator.Send(query, ct);

        if (result.IsSuccess)
        {
            // Map the result to the TrendsResponse
            Response = new TrendsResponse
            {
                Message = "Success",
                trends = result.Value
            };
        }
        else
        {
            // Handle failure (error message)
            Response = new TrendsResponse
            {
                Message = "Failed",
                trends = new List<TrendsDTO>() // Return an empty list in case of error
            };
        }
    }
}