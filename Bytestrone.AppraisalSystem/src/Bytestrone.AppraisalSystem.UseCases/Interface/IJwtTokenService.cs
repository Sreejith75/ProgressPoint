namespace Bytestrone.AppraisalSystem.UseCases.Interface;
public interface IJwtTokenService
{
    string GenerateToken(string userId, string username, IEnumerable<string> roles, List<string> permissions, int EmployeeRoleId);
}