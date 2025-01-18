namespace Bytestrone.AppraisalSystem.Web.Employees;
public class CreateEmployeeRequest
{
    public const string Route = "Employees"; 

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string? Email { get; set; }

    public string? PasswordHash { get; set; }

    public int EmployeeRoleId { get; set; }

    public List<int> SystemRoleIds { get; set; } = new List<int>(); 

    public string? PhoneNumber { get; set; }

    public int AppraiserId { get; set; }
}