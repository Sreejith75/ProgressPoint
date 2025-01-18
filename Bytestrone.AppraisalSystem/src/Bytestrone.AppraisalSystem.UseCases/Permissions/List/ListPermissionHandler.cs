using Ardalis.Result;
using Ardalis.SharedKernel;
using Ardalis.Specification;
using Bytestrone.AppraisalSystem.Core.Entities.SystemRoleAggregate.Specification;
using Bytestrone.AppraisalSystem.Core.PermissionAggregate;

namespace Bytestrone.AppraisalSystem.UseCases.Permissions.List;
public class ListPermissionQueryHandler(IListPermissionQueryService _query) : IQueryHandler<ListPermissionQuery, Result<IEnumerable<PermissionDTO>>>
{

  public async Task<Result<IEnumerable<PermissionDTO>>> Handle(ListPermissionQuery request, CancellationToken cancellationToken)
    {
     var result =await _query.ListAsync();
     return Result.Success(result);  
    }
}
