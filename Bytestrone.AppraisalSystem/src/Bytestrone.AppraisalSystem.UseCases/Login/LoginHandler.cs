using Ardalis.Result;
using Ardalis.SharedKernel;
using Bytestrone.AppraisalSystem.UseCases.Interface;
namespace Bytestrone.AppraisalSystem.UseCases.Login;
public class LoginHandler(ILoginService loginService) : ICommandHandler<LoginCommand, Result<LoginResponseDto>>
{
    private readonly ILoginService _loginService = loginService;

    public async Task<Result<LoginResponseDto>> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var loginResponse = await _loginService.LoginAsync(request.Email, request.Password);

            return Result<LoginResponseDto>.Success(loginResponse);
        }
        catch (UnauthorizedAccessException ex)
        {
            return Result<LoginResponseDto>.Error(ex.Message);
        }
    }
}
