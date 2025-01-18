using Bytestrone.AppraisalSystem.UseCases.Departments;

namespace Bytestrone.AppraisalSystem.web.Departments;
public class ListDepartmentsResponse
{
    public string? Message { get; set; }
    public List<DepartmentDTO>? Departments { get; set; }
}