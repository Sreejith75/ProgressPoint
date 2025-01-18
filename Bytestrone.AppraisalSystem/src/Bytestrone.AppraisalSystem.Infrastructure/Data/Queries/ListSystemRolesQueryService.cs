using Bytestrone.AppraisalSystem.UseCases.SystemRoles;
using Bytestrone.AppraisalSystem.UseCases.SystemRoles.List;
using Bytestrone.AppraisalSystem.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Crypto.Agreement.Srp;
using Bytestrone.AppraisalSystem.UseCases.Permissions;

namespace Bytestrone.AppraisalSystem.Infrastructure.Data.Queries;

public class ListSystemRoleQueryService : IListSystemRoleQueryService
{
    private readonly AppDbContext _db;

    public ListSystemRoleQueryService(AppDbContext db)
    {
        _db = db;
    }

    public async Task<IEnumerable<SystemRoleDTO>> ListAsync()
    {
        var roles = await _db.SystemRoles
            .Include(role => role.SystemRolePermissions)
            .ThenInclude(srp => srp.Permission) 
            .Select(role => new SystemRoleDTO(
                role.Id,
                role.RoleName,
                role.Description,
                role.SystemRolePermissions.Select(srp => new PermissionDTO(srp.PermissionId,srp.Permission.PermissionName)).ToList() 
            ))
            .ToListAsync();

        return roles;
    }
}
