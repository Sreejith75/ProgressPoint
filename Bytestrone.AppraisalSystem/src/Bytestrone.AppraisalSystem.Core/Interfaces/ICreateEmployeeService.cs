
namespace Bytestrone.AppraisalSystem.Core.Interfaces;
public interface ICreateEmployeeService
{
    Task<int> CreateEmployeeAsync(
        string firstName,
        string lastName,
        string email,
        string password,
        string? phoneNumber,
        int employeeRoleId,
        IEnumerable<int> systemRoleIds,
        int? appraiserId,
        CancellationToken cancellationToken);
}

