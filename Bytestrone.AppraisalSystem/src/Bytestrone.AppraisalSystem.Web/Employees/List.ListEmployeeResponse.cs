using Bytestrone.AppraisalSystem.UseCases.Employees.List;

namespace Bytestrone.AppraisalSystem.web.Employees;
public class ListEmployeeResponse
{
    public string? Message { get; set; }
    public List<EmployeeDetailsDisplayDTO>? employeeDetails { get; set; }
}