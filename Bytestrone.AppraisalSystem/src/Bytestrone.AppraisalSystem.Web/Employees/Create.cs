using Bytestrone.AppraisalSystem.UseCases.Employees.Create;
using FastEndpoints;
using MediatR;

namespace Bytestrone.AppraisalSystem.Web.Employees;
public class Create(IMediator mediator) : Endpoint<CreateEmployeeRequest, CreateEmployeeResponse>
{

  public override void Configure()
    {
        Post(CreateEmployeeRequest.Route);
        AllowAnonymous();
        Summary(summary =>
            summary.ExampleRequest = new CreateEmployeeRequest
            {
                FirstName = "John",
                LastName = "Doe",
                Email = "john.doe@example.com",
                PasswordHash = "hashedpassword123",
                EmployeeRoleId = 1,
                SystemRoleIds = new List<int> { 1, 2 },
                PhoneNumber = "123-456-7890",
                AppraiserId = 2
            });
    }

    public override async Task HandleAsync(CreateEmployeeRequest request, CancellationToken cancellationToken)
    {

        var command = new CreateEmployeeCommand(
            FirstName: request.FirstName!,
            LastName: request.LastName!,
            Email: request.Email!,
            Password: request.PasswordHash!,
            EmployeeRoleId: request.EmployeeRoleId,
            SystemRoleIds: request.SystemRoleIds ?? new List<int>(),
            PhoneNumber: request.PhoneNumber,
            AppraiserId: request.AppraiserId
        );

        var result = await mediator.Send(command, cancellationToken);

        if (result.IsSuccess)
        {
            var response = new CreateEmployeeResponse(result.Value);

            await SendOkAsync(response, cancellationToken);
        }
    }
}
