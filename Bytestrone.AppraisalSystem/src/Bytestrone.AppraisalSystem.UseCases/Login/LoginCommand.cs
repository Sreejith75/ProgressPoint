using Ardalis.Result;

namespace Bytestrone.AppraisalSystem.UseCases.Login;
public class LoginCommand(string email, string password) : Ardalis.SharedKernel.ICommand<Result<LoginResponseDto>>
{
  public string Email { get; set; } = email;
  public string Password { get; set; } = password;
}
