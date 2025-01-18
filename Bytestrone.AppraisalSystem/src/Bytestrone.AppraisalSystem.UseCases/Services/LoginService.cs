using Ardalis.SharedKernel;
using Bytestrone.AppraisalSystem.Core.EmployeeAggregate;
using Bytestrone.AppraisalSystem.Core.Interfaces;
using Bytestrone.AppraisalSystem.Core.SystemRoleAggregate;
using Bytestrone.AppraisalSystem.UseCases.Interface;
using Bytestrone.AppraisalSystem.UseCases.Login;

public class LoginService(
    IRepository<Employee> employeeRepository,
    IJwtTokenService jwtTokenService,
    IHashingService hashingService,
    IRepository<SystemRole> systemRoleRepository) : ILoginService
{
    private readonly IRepository<Employee> _employeeRepository = employeeRepository;
    private readonly IRepository<SystemRole> _systemRoleRepository = systemRoleRepository;
    private readonly IJwtTokenService _jwtTokenService = jwtTokenService;
    private readonly IHashingService _hashingService = hashingService;

    public async Task<LoginResponseDto> LoginAsync(string email, string password)
    {
        var employee = await _employeeRepository.FirstOrDefaultAsync(new EmployeeWithRolesSpecification(email));

        if (employee == null || !_hashingService.VerifyPassword(employee.PasswordHash, password))
        {
            throw new UnauthorizedAccessException("Invalid email or password.");
        }

        var systemRoleNames = employee.SystemRoles
            .Where(esr => esr.SystemRole != null)
            .Select(esr => esr.SystemRole!.RoleName)
            .ToList();

        var rolePermissions = employee.GetPermissions();

        var token = _jwtTokenService.GenerateToken(
            employee.Id.ToString(),
            employee.GetFullName(),
            systemRoleNames,
            rolePermissions!,
            employee.EmployeeRoleId);

        return new LoginResponseDto(token);
    }
}
