namespace Bytestrone.AppraisalSystem.UseCases.EmployeeRoles;
public record EmployeeRoleDTO
{
    public int Id { get; set; }
    public string? RoleName { get; set; }
    public string? RoleCode { get; set; }
    public int HierarchyLevel { get; set; }
    public int DepartmentId { get; set; }
}