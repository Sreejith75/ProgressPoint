using Ardalis.Result;
using Ardalis.SharedKernel;

namespace Bytestrone.AppraisalSystem.UseCases.Permissions.List;
public record ListPermissionQuery : IQuery<Result<IEnumerable<PermissionDTO>>>;