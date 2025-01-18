using Bytestrone.AppraisalSystem.UseCases.AppraisalFeedbacks;
using Bytestrone.AppraisalSystem.UseCases.AppraisalFeedbacks.CreateFeedback;
using Bytestrone.AppraisalSystem.web.AppraisalFeedbacks;
using FastEndpoints;
using MediatR;

namespace Bytestrone.AppraisalSystem.Web.AppraisalFeedbacks;

public class Create(IMediator mediator) : Endpoint<AppraisalFeedbackRequest, AppraisalFeedbackResponse>
{
    private readonly IMediator _mediator = mediator;

    public override void Configure()
    {
        Post("/appraisal-feedback");
        AllowAnonymous();
    }

    public override async Task HandleAsync(AppraisalFeedbackRequest req, CancellationToken ct)
    {
        var command = new CreateAppraisalFeedbackCommand( req.EmployeeId, req.CycleId);
        var result = await _mediator.Send(command, ct);
        if (result.IsSuccess)
            await SendOkAsync(new AppraisalFeedbackResponse(result.Value, true), ct);
        else
            await SendAsync(new AppraisalFeedbackResponse(result.Value, false), cancellation: ct);
    }

}
