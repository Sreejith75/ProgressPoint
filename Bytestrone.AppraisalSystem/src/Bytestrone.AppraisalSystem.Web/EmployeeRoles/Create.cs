using Bytestrone.AppraisalSystem.UseCases.EmployeeRoles.Create;
using FastEndpoints;
using MediatR;

namespace Bytestrone.AppraisalSystem.Web.EmployeeRoles;
public class Create(IMediator _mediator) : Endpoint<CreateEmployeeRoleRequest,CreateEmployeeRoleResponse>
{
    public override void Configure()
    {
        Post(CreateEmployeeRoleRequest.Route);
        AllowAnonymous();
        Summary(s=>s.ExampleRequest=new CreateEmployeeRoleRequest
        {
            RoleName="Junior fullstack developer",
            EmployeeRoleCode="JRFSE",
            HierarchyLevel=1,
            DepartmentId=1
        });
    }
    public override async Task HandleAsync(CreateEmployeeRoleRequest request, CancellationToken cancellationToken)
    {
        var result =await _mediator.Send(new CreateEmployeeRoleCommand(request.RoleName!,request.EmployeeRoleCode!,request.HierarchyLevel, request.DepartmentId),cancellationToken);
        if (result.IsSuccess)
        {
            Response =new CreateEmployeeRoleResponse(result.Value);
            return;
        }
    }
}