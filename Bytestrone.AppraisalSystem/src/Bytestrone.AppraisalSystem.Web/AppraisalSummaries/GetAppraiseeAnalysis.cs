using System.Net;
using Bytestrone.AppraisalSystem.UseCases.AppraisalSummaries.AppraiseeAnalysis;
using FastEndpoints;
using MediatR;

namespace Bytestrone.AppraisalSystem.web.AppraisalSummaries;

public class GetAppraiseeAnalysis(IMediator mediator) : Endpoint<AppraiseeFilterRequest, AppraiseeAnalysisResponse>
{
    public override void Configure()
    {
        Get("/AppraisalSummary/appraisee-analysis");
        AllowAnonymous();
    }

    public override async Task HandleAsync(AppraiseeFilterRequest req, CancellationToken ct)
    {
        var query = new AppraiseeAnalysisQuery(
            req.Quarter,
            req.Year!,
            req.DepartmentId,
            req.RoleId
        );

        var result = await mediator.Send(query, ct);

        if (result.IsSuccess)
        {
            Response = new AppraiseeAnalysisResponse
            {
                Message = "Success",
                AppraiseeAnalysis = result.Value
            };
            await SendOkAsync(Response, ct);
        }
        else
        {
            Response = new AppraiseeAnalysisResponse
            {
                Message = "Failed"
            };
            return;
        }
    }
}
