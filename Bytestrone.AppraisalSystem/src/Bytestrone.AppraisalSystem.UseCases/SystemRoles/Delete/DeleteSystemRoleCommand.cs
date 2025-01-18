using Ardalis.Result;
using Ardalis.SharedKernel;

namespace Bytestrone.AppraisalSystem.UseCases.SystemRoles.Delete;

public record DeleteSystemRoleCommand(int SystemRoleId) : ICommand<Result>;
