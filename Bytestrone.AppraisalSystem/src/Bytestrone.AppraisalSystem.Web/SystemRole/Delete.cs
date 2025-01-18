using Ardalis.Result;
using Bytestrone.AppraisalSystem.UseCases.SystemRoles.Delete;
using FastEndpoints;
using MediatR;

namespace Bytestrone.AppraisalSystem.Web.SystemRole;

public class Delete(IMediator _mediator)
  : Endpoint<DeleteSystemRoleRequest>
{
    public override void Configure()
    {
        Delete(DeleteSystemRoleRequest.Route);
        AllowAnonymous();
    }

    public override async Task HandleAsync(
      DeleteSystemRoleRequest request,
      CancellationToken cancellationToken)
    {
        var command = new DeleteSystemRoleCommand(request.SystemRoleId);

        var result = await _mediator.Send(command, cancellationToken);

        if (result.Status == ResultStatus.NotFound)
        {
            await SendNotFoundAsync(cancellationToken);
            return;
        }

        if (result.IsSuccess)
        {
            await SendOkAsync("System Role Successfully Deleted", cancellationToken);
        };
    }
}
