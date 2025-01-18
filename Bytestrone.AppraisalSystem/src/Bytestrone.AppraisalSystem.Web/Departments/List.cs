using Bytestrone.AppraisalSystem.UseCases.Departments.List;
using FastEndpoints;
using Google.Apis.Download;
using MediatR;

namespace Bytestrone.AppraisalSystem.web.Departments;
public class List(IMediator mediator): EndpointWithoutRequest<ListDepartmentsResponse>
{
  public override void Configure()
  {
    Get("Departments");
    AllowAnonymous();
  }
  public override async Task HandleAsync(CancellationToken ct)
  {
    var query = new ListDepartmentsQuery();
    var result = await mediator.Send(query,ct);
    if(result.IsSuccess)
    {
        Response = new ListDepartmentsResponse
        {
            Message="Success",
            Departments=result.Value
        };
        return;
    }
    else
    {
        Response = new ListDepartmentsResponse
        {
            Message="Failed"
        };
        return;
    }
    
  }
}