using Ardalis.Result;
using Bytestrone.AppraisalSystem.UseCases.Permissions;
using Bytestrone.AppraisalSystem.UseCases.Permissions.List;
using FastEndpoints;
using MediatR;

namespace Bytestrone.AppraisalSystem.Web.Permissions;

/// <summary>
/// List all Permissions for a specific Role
/// </summary>
/// <remarks>
/// List all permissions for a role based on the role Id.
/// </remarks>
public class ListPermissionEndpoint(IMediator mediator) : EndpointWithoutRequest<List<PermissionDTO>>
{
    private readonly IMediator _mediator = mediator;

    public override void Configure()
    {
        Get("/permissions");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CancellationToken cancellationToken)
    {
        Result<IEnumerable<PermissionDTO>> result = await _mediator.Send(new ListPermissionQuery(), cancellationToken);

        if (result.IsSuccess)
        {
            Response = result.Value.Select(c => new PermissionDTO(c.Id, c.PermissionName)).ToList();
        }
    }
}
