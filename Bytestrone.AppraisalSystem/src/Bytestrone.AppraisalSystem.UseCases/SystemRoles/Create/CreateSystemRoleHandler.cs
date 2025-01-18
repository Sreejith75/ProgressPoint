using Ardalis.GuardClauses;
using Ardalis.Result;
using Ardalis.SharedKernel;
using Bytestrone.AppraisalSystem.Core.Entities.PermissionAggregate.Specification;
using Bytestrone.AppraisalSystem.Core.PermissionAggregate;
using Bytestrone.AppraisalSystem.Core.SystemRoleAggregate;
namespace Bytestrone.AppraisalSystem.UseCases.SystemRoles.Create;
public class CreateSystemRoleCommandHandler(
    IRepository<SystemRole> systemRoleRepository,
    IRepository<Permission> permissionRepository) : ICommandHandler<CreateSystemRoleCommand, Result<int>>
{
    private readonly IRepository<SystemRole> _systemRoleRepository = Guard.Against.Null(systemRoleRepository, nameof(systemRoleRepository));
    private readonly IRepository<Permission> _permissionRepository = Guard.Against.Null(permissionRepository, nameof(permissionRepository));

    public async Task<Result<int>> Handle(CreateSystemRoleCommand request, CancellationToken cancellationToken)
    {
        Guard.Against.Null(request, nameof(request));

        List<Permission> permissions = [];

        if (request.PermissionIds != null && request.PermissionIds.Count>0)
        {
            var permissionSpec = new GetPermissionByIdsSpec(request.PermissionIds);
            permissions = await _permissionRepository.ListAsync(permissionSpec, cancellationToken);

            var missingPermissions = request.PermissionIds.Except(permissions.Select(p => p.Id)).ToList();
            if (missingPermissions.Count > 0)
            {
                return Result.Error($"The following permissions were not found: {string.Join(", ", missingPermissions)}");
            }
        }

        var systemRole = new SystemRole(request.SystemRoleName, request.Description);

        foreach (var permission in permissions)
        {
            systemRole.AddPermission(permission);
        }

        await _systemRoleRepository.AddAsync(systemRole, cancellationToken);

        return Result.Success(systemRole.Id);
    }

}
