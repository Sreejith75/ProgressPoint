using Bytestrone.AppraisalSystem.UseCases.AppraisalFeedbacks;
using Bytestrone.AppraisalSystem.UseCases.AppraisalFeedbacks.GetHistory;
using Bytestrone.AppraisalSystem.web.AppraisalFeedbacks;
using Bytestrone.AppraisalSystem.Web.AppraisalFeedbacks;
using FastEndpoints;
using MediatR;

namespace Bytestrone.AppraisalSystem.Web.AppraisalFeedbacks;

public class GetHistory(IMediator mediator) : Endpoint<GetAppraisalHistoryRequest, GetAppraisalHistoryResponse>
{
    private readonly IMediator _mediator = mediator;

    public override void Configure()
    {
        Get("/appraisal-feedback/{EmployeeId:int}");
        AllowAnonymous();
    }

    public override async Task HandleAsync(GetAppraisalHistoryRequest req, CancellationToken ct)
    {
        var query = new GetAppraisalHistoryQuery(req.EmployeeId);
        var result = await _mediator.Send(query, ct);

        if (result.IsSuccess && result.Value != null)
        {
            Response = new GetAppraisalHistoryResponse
            {
                FeedbackHistory = result.Value.Select(r => new FeedbackHistoryDTO
                {
                    FeedbackId = r.FeedbackId,
                    Quarter = r.Quarter,
                    Year = r.Year,
                    AppraiseeScore = r.AppraiseeScore,
                    AppraiserScore =r.AppraiserScore,
                    PerformanceBucketName = r.PerformanceBucketName,
                    AssessmentStatus=r.AssessmentStatus
                }).ToList(),
                Status = "Success"
            };

            await SendOkAsync(Response, ct);
        }
        else
        {
            Response = new GetAppraisalHistoryResponse
            {
                FeedbackHistory = null,
                Status = "Error",
                ErrorMessage = result.Errors?.FirstOrDefault() ?? "Failed to fetch appraisal history."
            };

            await SendAsync(Response, 400, ct);
        }
    }
}
