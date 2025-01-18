using Bytestrone.AppraisalSystem.UseCases.PerformanceFactors.ListFactorsForRoles;
using FastEndpoints;
using MediatR;

namespace Bytestrone.AppraisalSystem.web.PerformanceFactors;
public class ListFactorsForDepartent(IMediator mediator) : EndpointWithoutRequest<ListFactorsForDepartmentResponse>
{
  public override void Configure()
  {
    Get("/PerfomanceFactors/departments");
    AllowAnonymous();
  }
  public override async Task HandleAsync(CancellationToken ct)
  {
    var query = new ListFactorsForDepartmentsQuery();
    var result = await mediator.Send(query, ct);
    if (result.IsSuccess)
    {
      Response = new ListFactorsForDepartmentResponse
      {
        Message = "Success",
        Departments = result.Value
      };
      return;
    }
    else
    {
      Response = new ListFactorsForDepartmentResponse
      {
        Message = "Failed"
      };
      return;
    }
  }
}