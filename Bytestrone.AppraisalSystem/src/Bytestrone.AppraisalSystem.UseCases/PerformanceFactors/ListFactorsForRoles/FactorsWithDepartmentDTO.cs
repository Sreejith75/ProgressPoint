namespace Bytestrone.AppraisalSystem.UseCases.PerformanceFactors.ListFactorsForRoles;
public class FactorsWithDepartmentsDTO
{
    public int DepartmentId { get; set; }
    public string? DepartmentName { get; set; }
    public List<FactorDTO>? factors { get; set; }
}

public class FactorDTO
{
    public int FactorId { get; set; }
    public string? FactorName { get; set; }
    public decimal Weightage { get; set; }
}