using FastEndpoints;
using MediatR;
using Bytestrone.AppraisalSystem.UseCases.Login; // Adjust the namespace if needed
using Ardalis.Result;
using Bytestrone.AppraisalSystem.UseCases.Services;
using System.Threading.Tasks;
using System.Threading;

namespace Bytestrone.AppraisalSystem.Web.Login;
public class LoginEndpoint(IMediator mediator) : Endpoint<LoginRequest, LoginResponse>
{
    private readonly IMediator _mediator = mediator;

  public override void Configure()
    {
        Post(LoginRequest.Route);
        AllowAnonymous();
    }

    public override async Task HandleAsync(LoginRequest req, CancellationToken ct)
    {
        // string decryptedEmail = DecryptService.Decrypt(req.Email);
        // string decryptedPassword = DecryptService.Decrypt(req.Password);

        // var loginCommand = new LoginCommand(decryptedEmail, decryptedPassword);

        var result = await _mediator.Send(new LoginCommand(req.Email,req.Password), ct);

        if (result.IsSuccess)
        {
            var loginResponse = new LoginResponse(result.Value.Token);

            await SendOkAsync(loginResponse, ct);
        }
        else
        {
            await SendUnauthorizedAsync(ct);
        }
    }
}