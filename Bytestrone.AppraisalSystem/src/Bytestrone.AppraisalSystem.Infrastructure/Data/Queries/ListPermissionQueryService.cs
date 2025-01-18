using Ardalis.Specification.EntityFrameworkCore;
using Bytestrone.AppraisalSystem.UseCases.Permissions;
using Bytestrone.AppraisalSystem.UseCases.Permissions.List;
using Microsoft.EntityFrameworkCore;

namespace Bytestrone.AppraisalSystem.Infrastructure.Data.Queries;
public class ListPermissionQueryService(AppDbContext _db) :IListPermissionQueryService
{
    public async Task<IEnumerable<PermissionDTO>> ListAsync()
    {
        var result =await _db.Permissions.Select(permission=> new PermissionDTO(
            permission.Id,
            permission.PermissionName
        )).ToListAsync();
        return result;
    }
}