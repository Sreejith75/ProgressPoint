namespace Bytestrone.AppraisalSystem.UseCases.Employees;
public class EmployeeDTO(
        string firstName,
        string lastName,
        string email,
        string password,
        string? phoneNumber,
        int employeeRoleId,
        IEnumerable<int> systemRoleIds,
        int? appraiserId,
        string createdBy)
{
  public string FirstName { get; set; } = firstName;
  public string LastName { get; set; } = lastName;
  public string Email { get; set; } = email;
  public string Password { get; set; } = password;
  public string? PhoneNumber { get; set; } = phoneNumber;
  public int EmployeeRoleId { get; set; } = employeeRoleId;
  public IEnumerable<int> SystemRoleIds { get; set; } = systemRoleIds;
  public int? AppraiserId { get; set; } = appraiserId;
  public string CreatedBy { get; set; } = createdBy;
}