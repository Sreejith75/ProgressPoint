using Ardalis.Result;

namespace Bytestrone.AppraisalSystem.UseCases.SystemRoles.Create;
public record CreateSystemRoleCommand(string SystemRoleName,string Description, List<int>? PermissionIds):Ardalis.SharedKernel.ICommand<Result<int>>;
