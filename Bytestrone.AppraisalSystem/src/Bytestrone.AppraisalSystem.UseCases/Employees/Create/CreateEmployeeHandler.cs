using Ardalis.GuardClauses;
using Ardalis.Result;
using Ardalis.SharedKernel;
using Bytestrone.AppraisalSystem.Core.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace Bytestrone.AppraisalSystem.UseCases.Employees.Create;

public class CreateEmployeeCommandHandler(ICreateEmployeeService createEmployeeService) : ICommandHandler<CreateEmployeeCommand, Result<int>>
{
    private readonly ICreateEmployeeService _createEmployeeService = Guard.Against.Null(createEmployeeService, nameof(createEmployeeService));

    public async Task<Result<int>> Handle(CreateEmployeeCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var employeeId = await _createEmployeeService.CreateEmployeeAsync(
                request.FirstName,
                request.LastName!,
                request.Email!,
                request.Password!,
                request.PhoneNumber,
                request.EmployeeRoleId,
                request.SystemRoleIds!,
                request.AppraiserId,
                cancellationToken
            );

            return Result<int>.Success(employeeId);
        }
        catch (Exception ex)
        {
            return Result<int>.Error($"Failed to create employee: {ex.Message}");
        }
    }
}
