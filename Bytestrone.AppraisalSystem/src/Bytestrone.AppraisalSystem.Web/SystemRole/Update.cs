using Bytestrone.AppraisalSystem.UseCases.SystemRoles.Update;
using FastEndpoints;
using MediatR;

namespace Bytestrone.AppraisalSystem.web.SystemRole;
public class Update(IMediator mediator) : Endpoint<UpdateSystemRoleRequest, UpdateSyatemRoleResponse>
{
  public override void Configure()
  {
    Put("/system-roles/permission");
    AllowAnonymous();
  }
  public override async Task HandleAsync(UpdateSystemRoleRequest req, CancellationToken ct)
  {
    var command = new UpdateSystemRoleCommand(req.SystemRoleId, req.PermissionIds!);
    var result = await mediator.Send(command, ct);
    if (result.IsSuccess)
    {
      Response = new UpdateSyatemRoleResponse
      {
        Message = "Success",
        Id = result.Value
      };
    }
    else
    {
      Response = new UpdateSyatemRoleResponse
      {
        Message = "Failed"
      };
    }
  }
}