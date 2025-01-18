using Ardalis.Result;
using Bytestrone.AppraisalSystem.UseCases.Contributors.Create;
using Bytestrone.AppraisalSystem.UseCases.SystemRoles.Create;
using FastEndpoints;
using MediatR;

namespace Bytestrone.AppraisalSystem.Web.SystemRole;
public class Create(IMediator _mediator) : Endpoint<CreateSystemRoleRequest, CreateSystemRoleResponse>
{
  public override void Configure()
  {
    Post(CreateSystemRoleRequest.Route);
    AllowAnonymous();
    Summary(s => s.ExampleRequest = new CreateSystemRoleRequest
    {
      SystemRoleName = "Admin",
      Description = "",
      Permissions = [1, 2]
    });
  }
  public override async Task<Result<int>> HandleAsync(CreateSystemRoleRequest reqest, CancellationToken cancellationToken)
  {
    var result = await _mediator.Send(new CreateSystemRoleCommand(reqest.SystemRoleName!, reqest.Description!, reqest.Permissions), cancellationToken);
    if (result.IsSuccess)
    {
      var response = new CreateSystemRoleResponse(result.Value);
      await SendAsync(response, statusCode: 201, cancellationToken);
    }
    else
    {
      AddError(string.Join(", ", result.Errors ?? new[] { "An error occurred while creating the system role." }));
      await SendErrorsAsync(statusCode: 400, cancellationToken);
    }
    return result;
  }
}