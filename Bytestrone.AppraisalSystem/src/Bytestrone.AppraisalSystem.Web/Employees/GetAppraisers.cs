using Bytestrone.AppraisalSystem.UseCases.Employees.GetAppraisersList;
using FastEndpoints;
using MediatR;

namespace Bytestrone.AppraisalSystem.web.Employees;
public class GetAppraisers(IMediator mediator):Endpoint<GetAppraisersRequest,GetAppraisersResponse>
{
  public override void Configure()
  {
    Get("/Employees/appraisers-list/{RoleId:int}");
    AllowAnonymous();
  }
  public override async Task HandleAsync(GetAppraisersRequest req, CancellationToken ct)
  {
    var command = new GetAppraisersQuery(req.RoleId);
    var result = await mediator.Send(command,ct);
    if (result.IsSuccess)
    {
        Response= new GetAppraisersResponse
        {
            Message="Success",
            appraisers=result.Value,
        };
        return;
    }
    else
    {
        Response =new GetAppraisersResponse
        {
            Message="Failed"
        };
        return ;
    }
  }
}