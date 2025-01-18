
using Ardalis.Result;
namespace Bytestrone.AppraisalSystem.UseCases.Employees.Create;
public record CreateEmployeeCommand : Ardalis.SharedKernel.ICommand<Result<int>>
{
    public string FirstName { get; set; }
    public  string LastName { get; set; }
    public  string Email { get; set; }
    public  string Password { get; set; }
    public string? PhoneNumber { get; set; }
    public  int EmployeeRoleId { get; set; }
    public  IEnumerable<int> SystemRoleIds { get; set; }
    public int? AppraiserId { get; set; }

    public CreateEmployeeCommand(
        string FirstName,
        string LastName,
        string Email,
        string Password,
        string? PhoneNumber,
        int EmployeeRoleId,
        IEnumerable<int> SystemRoleIds,
        int? AppraiserId)
    {
        this.FirstName = FirstName;
        this.LastName = LastName;
        this.Email = Email;
        this.Password = Password;
        this.PhoneNumber = PhoneNumber;
        this.EmployeeRoleId = EmployeeRoleId;
        this.SystemRoleIds = SystemRoleIds;
        this.AppraiserId = AppraiserId;
    }
}
