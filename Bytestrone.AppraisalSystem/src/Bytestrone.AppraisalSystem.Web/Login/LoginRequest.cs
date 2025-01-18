namespace Bytestrone.AppraisalSystem.Web.Login;

public class LoginRequest
{
    public const string Route="/user/login";

    public required string Email {get; set;} 
    public required string Password {get; set;} 
}