using Ardalis.Result;
using Ardalis.SharedKernel;
using Bytestrone.AppraisalSystem.Core.PermissionAggregate;
using Bytestrone.AppraisalSystem.Core.SystemRoleAggregate;
using MediatR;

namespace Bytestrone.AppraisalSystem.UseCases.SystemRoles.Update;

public class UpdateSystemRoleCommandHandler(IRepository<SystemRole> systemRoleRepository, IRepository<Permission> permissionRepository) : ICommandHandler<UpdateSystemRoleCommand, Result<int>>
{
    private readonly IRepository<SystemRole> _systemRoleRepository = systemRoleRepository ?? throw new ArgumentNullException(nameof(systemRoleRepository));
    private readonly IRepository<Permission> _permissionRepository = permissionRepository ?? throw new ArgumentNullException(nameof(permissionRepository));

    public async Task<Result<int>> Handle(UpdateSystemRoleCommand request, CancellationToken cancellationToken)
    {
        var systemRole = await _systemRoleRepository.GetByIdAsync(request.SystemRoleId, cancellationToken);
        if (systemRole == null)
        {
            return Result<int>.NotFound();
        }

        var permissions = new List<Permission>();
        foreach (var permissionId in request.PermissionIds)
        {
            var permission = await _permissionRepository.GetByIdAsync(permissionId, cancellationToken);
            if (permission != null)
            {
                permissions.Add(permission);
            }
        }

        try
        {
            systemRole.UpdatePermissions(permissions);

            await _systemRoleRepository.UpdateAsync(systemRole, cancellationToken);

            return Result<int>.Success(systemRole.Id);
        }
        catch (Exception ex)
        {
            return Result<int>.Error(ex.Message);
        }
    }
}
