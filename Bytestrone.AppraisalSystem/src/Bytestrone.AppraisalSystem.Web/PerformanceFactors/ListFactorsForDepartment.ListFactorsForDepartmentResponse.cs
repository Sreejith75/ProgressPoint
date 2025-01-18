using Bytestrone.AppraisalSystem.UseCases.PerformanceFactors.ListFactorsForRoles;

namespace Bytestrone.AppraisalSystem.web.PerformanceFactors;
public class ListFactorsForDepartmentResponse
{
    public string? Message { get; set; }
    public List<FactorsWithDepartmentsDTO>? Departments { get; set; }

}