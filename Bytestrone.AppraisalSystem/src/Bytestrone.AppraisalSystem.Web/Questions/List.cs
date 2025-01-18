using FastEndpoints;
using MediatR;
using Bytestrone.AppraisalSystem.UseCases.Questions;
using Bytestrone.AppraisalSystem.UseCases.Questions.List;

namespace Bytestrone.AppraisalSystem.Api.Endpoints.Questions;

public class ListQuestionsEndpoint(IMediator mediator) : EndpointWithoutRequest
{
    private readonly IMediator _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));

  public override void Configure()
    {
        Get("/Questions"); 
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var query = new ListQuestionQuery();
        var result = await _mediator.Send(query, ct);

        if (!result.IsSuccess || result.Value == null || !result.Value.Any())
        {
            await SendNotFoundAsync(ct); 
            return;
        }

        await SendOkAsync(result.Value, ct);
    }
}
