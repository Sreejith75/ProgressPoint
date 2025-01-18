using Bytestrone.AppraisalSystem.UseCases.AppraisalFeedbacks.UpdateAppraiserFeedback;
using Bytestrone.AppraisalSystem.web.AppraisalFeedbacks;
using FastEndpoints;
using MediatR;

namespace Bytestrone.AppraisalSystem.Web.AppraisalFeedbacks;

public class UpdateAppraiserFeedbackEndpoint : Endpoint<AppraiserFeedbackRequest, AppraiserFeedbackResponse>
{
    private readonly IMediator _mediator;

    public UpdateAppraiserFeedbackEndpoint(IMediator mediator)
    {
        _mediator = mediator;
    }

    public override void Configure()
    {
        Put("/appraisal-feedback/update/appraiser-feedbacks"); 
        AllowAnonymous(); 
    }

    public override async Task HandleAsync(AppraiserFeedbackRequest req, CancellationToken ct)
    {
        var command = new UpdateAppraiserFeedbackCommand
        (
          req.FeedbackId,req.AppraiserId,req.appraiserFeedbackDetails!
        );

        var result = await _mediator.Send(command, ct);

        if (result.IsSuccess)
        {
            Response=new AppraiserFeedbackResponse
            {
               Id=result.Value.Id,
               FinalScore=result.Value.FinalScore,
               PerformanceBucket=result.Value.PerformanceBucket,
               Message="Success",
            };
        }
        else
        {
            Response= new AppraiserFeedbackResponse
            {
              Id=result.Value.Id,
              Message="failed"
            };
        }
    }
}
