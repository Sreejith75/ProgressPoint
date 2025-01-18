using Ardalis.Result;
using Ardalis.SharedKernel;
namespace Bytestrone.AppraisalSystem.UseCases.SystemRoles.List;
public record ListSystemRoleQuery : IQuery<Result<IEnumerable<SystemRoleDTO>>>;
