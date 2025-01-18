using Bytestrone.AppraisalSystem.Web.Permissions;

namespace Bytestrone.AppraisalSystem.Web.SystemRole;

public record SystemRoleRecord(int Id, string SystemRoleName, string Description, IEnumerable<PermissionRecord> Permissions);