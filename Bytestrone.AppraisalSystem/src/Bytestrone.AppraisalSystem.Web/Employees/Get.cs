using Bytestrone.AppraisalSystem.UseCases.Employees.Get;
using FastEndpoints;
using Google.Apis.Download;
using MediatR;

namespace Bytestrone.AppraisalSystem.web.Employees;
public class Get(IMediator mediator) : Endpoint<EmployeeDetailsRequest, EmployeeDetailsResponse>
{
    public override void Configure()
    {
        Get("/Employees/details/{EmployeeId:int}");
        AllowAnonymous();
    }
    public override async Task HandleAsync(EmployeeDetailsRequest req, CancellationToken ct)
    {
        var command = new GetEmployeeDetailsQuery(req.EmployeeId);
        var result = await mediator.Send(command, ct);
        var Employee = result.Value;
        if (result.IsSuccess)
        {
            Response = new EmployeeDetailsResponse
            {
                Name = Employee.Name,
                Email = Employee.Email,
                Role = Employee.Role,
                PhoneNumber = Employee.PhoneNumber,
                Manager = Employee.Manager,
                Department = Employee.Department,
                AppraisalsCompleted = Employee.AppraisalsCompleted,
                AverageAppraisalScore = Employee.AverageAppraisalScore??0,
                PerformanceBucket = Employee.PerformanceBucket,
                status = true
            };
            return;
        }
        else
        {
            Response = new EmployeeDetailsResponse
            {
                status = false
            };
            return;
        }
    }
}