namespace Bytestrone.AppraisalSystem.Web.SystemRole;
public class CreateSystemRoleRequest
{
    public const string Route="/system-roles";
    public string? SystemRoleName { get; set; }
    public string? Description { get; set; }
    public List<int>? Permissions { get; set; }
}