using Bytestrone.AppraisalSystem.UseCases.Questions.Create;
using Bytestrone.AppraisalSystem.Web.Questions;

using FastEndpoints;
using MediatR;

namespace Bytestrone.AppraisalSystem.Web.Questions;

public class Create (IMediator _mediator)
  : Endpoint<CreateQuestionRequest, CreateQuestionResponse>
{
    public override void Configure()
    {
        Post(CreateQuestionRequest.Route);
        Summary(s =>
        {
            s.ExampleRequest = new CreateQuestionRequest {QuestionText="What is your feedback?",IndicatorId=1};
        });
    }

    public override async Task HandleAsync(
      CreateQuestionRequest request,
      CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new CreateQuestionCommand(request.QuestionText!,
          request.IndicatorId), cancellationToken);

        if (result.IsSuccess)
        {
            await SendCreatedAtAsync(nameof(Create), new { id = result.Value }, new CreateQuestionResponse(result.Value));
            return;
        }
    }
}
