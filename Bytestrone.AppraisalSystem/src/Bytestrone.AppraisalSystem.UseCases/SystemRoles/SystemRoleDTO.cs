using Bytestrone.AppraisalSystem.UseCases.Permissions;

namespace Bytestrone.AppraisalSystem.UseCases.SystemRoles;
public record SystemRoleDTO(int Id, string SystemRoleName, string Description, IEnumerable<PermissionDTO> Permissions); 