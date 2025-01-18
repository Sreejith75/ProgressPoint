using Ardalis.Result;
using Bytestrone.AppraisalSystem.UseCases.SystemRoles;
using Bytestrone.AppraisalSystem.UseCases.SystemRoles.List;
using FastEndpoints;
using MediatR;

namespace Bytestrone.AppraisalSystem.Web.SystemRole;
public class List(IMediator _mediator) : EndpointWithoutRequest<IEnumerable<SystemRoleDTO>>
{
    public override void Configure()
    {
        Get("/system-roles");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CancellationToken cancellationToken)
    {
        Result<IEnumerable<SystemRoleDTO>> result = await _mediator.Send(new ListSystemRoleQuery(), cancellationToken);

        if (result.IsSuccess)
        {
            Response = new List<SystemRoleDTO>(
                     result.Value.Select(c => new SystemRoleDTO(c.Id, c.SystemRoleName, c.Description, c.Permissions))
                 );
        }
    }
}
