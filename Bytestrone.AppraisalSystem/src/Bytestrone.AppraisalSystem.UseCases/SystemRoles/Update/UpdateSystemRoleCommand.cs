using Ardalis.Result;
using Ardalis.SharedKernel;

namespace Bytestrone.AppraisalSystem.UseCases.SystemRoles.Update;
public record UpdateSystemRoleCommand(int SystemRoleId, List<int> PermissionIds):ICommand<Result<int>>;