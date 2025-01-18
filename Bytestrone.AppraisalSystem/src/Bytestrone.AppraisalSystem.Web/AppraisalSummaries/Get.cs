using Bytestrone.AppraisalSystem.UseCases.AppraisalSummaries.Get;
using Bytestrone.AppraisalSystem.web.AppraisalSummaries;
using FastEndpoints;
using MediatR;

namespace Bytestrone.AppraisalSystem.Web.AppraisalSummaries;

public class GetAppraisalSummaryEndpoint(IMediator mediator) 
    : Endpoint<GetAppraisalSummeryRequest, GetAppraisalSummaryResponse>
{
    private readonly IMediator _mediator = mediator;

    public override void Configure()
    {
        Get("/AppraisalSummary/{EmployeeId:int}/{CycleId:int}");
        AllowAnonymous();
    }

    public override async Task HandleAsync(GetAppraisalSummeryRequest req, CancellationToken ct)
    {
        var query = new GetAppraisalSummeryQuery(req.EmployeeId, req.CycleId);
        var result = await _mediator.Send(query, ct);

        if (result.IsSuccess && result.Value != null)
        {
            Response = new GetAppraisalSummaryResponse
            {
                SummaryId = result.Value.SummaryId,
                AppraiseeScore = result.Value.AppraiseeScore,
                AppraiserScore = result.Value.AppraiserScore,
                PerformanceBucket = result.Value.PerformanceBucket,
                Description = result.Value.Description,
                Message = "Appraisal summary retrieved successfully.",

            };
            await SendOkAsync(Response, ct);
        }
        else
        {
            Response = new GetAppraisalSummaryResponse
            {
                Message = result.Errors?.FirstOrDefault() ?? "Failed to retrieve appraisal summary."
            };
            await SendAsync(Response, 400, ct); // 400 Bad Request
        }
    }
}
