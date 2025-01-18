using System.Net;
using Bytestrone.AppraisalSystem.UseCases.Employees.List;
using Bytestrone.AppraisalSystem.web.Employees;
using FastEndpoints;
using MediatR;

namespace Bytestrone.AppraisalSystem.Web.Employees;

public class List(IMediator mediator) : EndpointWithoutRequest<ListEmployeeResponse>
{
    private readonly IMediator _mediator = mediator;

  public override void Configure()
    {
        Get("/Employees");
        AllowAnonymous(); // Allows unauthenticated users to access this endpoint
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        // Send query to mediator
        var result = await _mediator.Send(new ListEmployeeQuery(), ct);

        if (result.IsSuccess)
        {
            // If successful, map result to the response DTO
            Response = new ListEmployeeResponse
            {
                Message= "Sucess",
                employeeDetails = result.Value // Map the list of EmployeeDetailsDisplayDTO
            };

            await SendOkAsync(Response, ct);
        }
        else
        {
            Response = new ListEmployeeResponse
            {
                Message="Failed"
            };
            return;
        }
    }
}
