using Bytestrone.AppraisalSystem.UseCases.AppraisalFeedbacks.Get;
using Bytestrone.AppraisalSystem.web.AppraisalFeedbacks;
using FastEndpoints;
using MediatR;
using Microsoft.Identity.Client;

namespace Bytestrone.AppraisalSystem.Web.AppraisalFeedbacks;

public class GetExistingAppraiseeFeedback(IMediator mediator) : Endpoint<AppraiseeFeedbackRequest, ExistingFeedbackResponse>
{
    private readonly IMediator _mediator = mediator;

    public override void Configure()
    {
        Get("/appraisal-feedback/existing-feedback/{EmployeeId:int}/{CycleId:int}");
        AllowAnonymous();
    }

    public override async Task HandleAsync(AppraiseeFeedbackRequest req, CancellationToken ct)
    {
        
        var query = new AppraisalFeedbackQuery(req.EmployeeId, req.CycleId);

        var result = await _mediator.Send(query, ct);

        if (result.IsSuccess)
        {
            var dto = result.Value;
            Response = new ExistingFeedbackResponse
            {
                Status=true,
                FeedbackId = dto.FeedbackId,
                EmployeeId = dto.EmployeeId,
                FeedbackStatus=dto.Status,
                FeedbackDetails = dto.FeedbackDetails.Select(detail => new AppraiseeFeedbackDetail
                {
                    QuestionId = detail.QuestionId,
                    AppraiseeRating = detail.AppraiseeRating,
                    AppraiseeComment = detail.AppraiseeComment
                }).ToList()
            };
        }
        else
        {
            
            Response= new ExistingFeedbackResponse
            {
                Status=false
            };
            return;
        }

    }
}
