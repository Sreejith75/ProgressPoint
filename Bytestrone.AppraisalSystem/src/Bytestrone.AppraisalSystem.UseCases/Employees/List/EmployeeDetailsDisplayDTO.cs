namespace Bytestrone.AppraisalSystem.UseCases.Employees.List;
public class EmployeeDetailsDisplayDTO
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Email { get; set; }
    public string? PhoneNumber { get; set; }
    public string? Role  { get; set; }
    public string? Department { get; set; }
    public string? Manager { get; set; }
}