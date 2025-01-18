using System.Net;
using Ardalis.Result;
using Ardalis.SharedKernel;
using Bytestrone.AppraisalSystem.Core.EmployeeAggregate;
using Bytestrone.AppraisalSystem.Core.Entities.EmployeeAggregate.Specification;

namespace Bytestrone.AppraisalSystem.UseCases.Employees.List;

public class ListEmployeeHandler : IQueryHandler<ListEmployeeQuery, Result<List<EmployeeDetailsDisplayDTO>>>
{
    private readonly IRepository<Employee> _employeeRepository;

    public ListEmployeeHandler(IRepository<Employee> employeeRepository)
    {
        _employeeRepository = employeeRepository ?? throw new ArgumentNullException(nameof(employeeRepository));
    }

    public async Task<Result<List<EmployeeDetailsDisplayDTO>>> Handle(ListEmployeeQuery request, CancellationToken cancellationToken)
    {
        try
        {
            // Optional: Apply a specification if filtering is needed
            var employeeListSpec = new EmployeeListSpec();
            var employeeList = await _employeeRepository.ListAsync(employeeListSpec, cancellationToken);
            if (employeeList == null || !employeeList.Any())
            {
                return Result<List<EmployeeDetailsDisplayDTO>>.NotFound("No employees found.");
            }

            // Transform employees into DTOs
            var employeeDetailsDtos = employeeList.Select(employee => new EmployeeDetailsDisplayDTO
            {
                Id = employee.Id,
                Name = employee.GetFullName(),
                Department = employee.Role?.Department?.DepartmentName,
                Role = employee.Role?.RoleName,
                Email = employee.Email,
                PhoneNumber=employee.PhoneNumber,
                Manager=employee.GetCurrentManagerName(),
            }).ToList();

            return Result<List<EmployeeDetailsDisplayDTO>>.Success(employeeDetailsDtos);
        }
        catch (Exception ex)
        {
            return Result<List<EmployeeDetailsDisplayDTO>>.Error($"An error occurred while fetching employees: {ex.Message}");
        }
    }
}
