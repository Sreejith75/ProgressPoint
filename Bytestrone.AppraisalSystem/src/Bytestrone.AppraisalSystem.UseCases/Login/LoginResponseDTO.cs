namespace Bytestrone.AppraisalSystem.UseCases.Login;
public class LoginResponseDto(string token)
{
    public string Token { get; set; } = token;
}
