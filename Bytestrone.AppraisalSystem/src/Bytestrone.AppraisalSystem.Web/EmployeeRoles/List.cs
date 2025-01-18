using FastEndpoints;
using MediatR;
using Bytestrone.AppraisalSystem.UseCases.EmployeeRoles.List;
using Ardalis.Result;
using Bytestrone.AppraisalSystem.web.EmployeeRoles;

namespace Bytestrone.AppraisalSystem.Web.EmployeeRoles;

public class ListEmployeeRolesEndpoint : EndpointWithoutRequest<IEnumerable<EmployeeRoleRecord>>
{
    private readonly IMediator _mediator;

    public ListEmployeeRolesEndpoint(IMediator mediator)
    {
        _mediator = mediator;
    }

    public override void Configure()
    {
        Get("/employeeroles");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var query = new ListEmployeeRolesQuery();
        var result = await _mediator.Send(query, ct);

        if (result.IsSuccess)
        {
            var response = result.Value.Select(dto => new EmployeeRoleRecord
            {
                Id = dto.Id,
                RoleName = dto.RoleName,
                HierarchyLevel = dto.HierarchyLevel,
                DepartmentId = dto.DepartmentId
            });

            await SendAsync(response, 200, ct);
        }
        else
        {
            await SendErrorsAsync(500, ct);
        }
    }
}
