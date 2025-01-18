using Bytestrone.AppraisalSystem.UseCases.Questions.DeActivate;
using Bytestrone.AppraisalSystem.web.Questions;
using FastEndpoints;
using MediatR;

namespace Bytestrone.AppraisalSystem.Web.Questions;

public class DeactivateQuestionEndpoint(IMediator mediator) : EndpointWithoutRequest<DeActivateQuestionResponse>
{
    private readonly IMediator _mediator = mediator;

    public override void Configure()
    {
        Put("/Questions/deactivate/{id}");
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var id = Route<int>("id");
        var result = await _mediator.Send(new DeActivateQuestionCommand(id), ct);

        if (result.IsSuccess)
        {
            await SendAsync(new DeActivateQuestionResponse(result.Value), cancellation: ct);
        }
        else
        {
            await SendNotFoundAsync(ct);
        }
    }
}
