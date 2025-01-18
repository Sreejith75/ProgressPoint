namespace Bytestrone.AppraisalSystem.web.SystemRole;
public class UpdateSystemRoleRequest
{
    public int SystemRoleId { get; set; } 
    public List<int>? PermissionIds  { get; set; }
}