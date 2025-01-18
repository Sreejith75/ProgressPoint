namespace Bytestrone.AppraisalSystem.Web.SystemRole;
public record DeleteSystemRoleRequest
{
    public const string Route = "/system-roles/{SystemRoleId:int}";
    public static string BuildRoute(int SystemRoleId) => Route.Replace("{SystemRoleId:int}", SystemRoleId.ToString());

    public int SystemRoleId { get; set; }
}