
using Bytestrone.AppraisalSystem.UseCases.Login;
namespace Bytestrone.AppraisalSystem.UseCases.Interface;
public interface ILoginService
{
    Task<LoginResponseDto> LoginAsync(string username, string password);
}