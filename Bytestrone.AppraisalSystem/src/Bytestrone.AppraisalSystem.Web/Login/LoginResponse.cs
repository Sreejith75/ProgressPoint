namespace Bytestrone.AppraisalSystem.Web.Login;
public class LoginResponse(string token)
{
    public string Token { get; set; }=token;
}