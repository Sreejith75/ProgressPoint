using Ardalis.Result;
using Ardalis.SharedKernel;

namespace Bytestrone.AppraisalSystem.UseCases.SystemRoles.List;
public class ListSystemRoleHandler(IListSystemRoleQueryService _query) : IQueryHandler<ListSystemRoleQuery, Result<IEnumerable<SystemRoleDTO>>>
{
    public async Task<Result<IEnumerable<SystemRoleDTO>>> Handle(ListSystemRoleQuery request, CancellationToken cancellationToken)
    {
        var result = await _query.ListAsync();
        return Result.Success(result);

    }
}