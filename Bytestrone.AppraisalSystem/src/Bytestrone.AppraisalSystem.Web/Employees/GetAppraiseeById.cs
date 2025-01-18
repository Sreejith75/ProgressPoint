using Bytestrone.AppraisalSystem.UseCases.Employees.GetAppraiseeList;
using FastEndpoints;
using MediatR;
using Ardalis.Result;
using System.Threading;
using System.Threading.Tasks;
using Bytestrone.AppraisalSystem.web.Employees;

namespace Bytestrone.AppraisalSystem.Web.Employees;

public class GetAppraiseeById : Endpoint<AppraiseeGetRequest, AppraiseeGetResponse>
{
    private readonly IMediator _mediator;

    public GetAppraiseeById(IMediator mediator)
    {
        _mediator = mediator;
    }

    public override void Configure()
    {
        // Defines the URL pattern for the endpoint
        Get("Employees/{AppraiserId:int}");
        AllowAnonymous();
    }

    public override async Task HandleAsync(AppraiseeGetRequest req, CancellationToken ct)
    {
        // Create the query object with the AppraiserId from the request
        var result = await _mediator.Send(new GetAppraiseeListQuery(req.AppraiserId), ct);

        // If the result is successful, map it to the response
        if (result.IsSuccess)
        {
            Response = new AppraiseeGetResponse
            {
                AppraiseeList = result.Value,
                Message = "Success"
            };

            return;
        }
        else
        {
            Response = new AppraiseeGetResponse
            {
                Message="Appraisee details not found"
            };
            
        }
    }
}
