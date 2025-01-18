using FastEndpoints;
using MediatR;
using Bytestrone.AppraisalSystem.UseCases.AppraisalForms.GetFormQuestions;
using Bytestrone.AppraisalSystem.web.AppraisalForms;
using Bytestrone.AppraisalSystem.UseCases.AppraisalForms;
using Microsoft.AspNetCore.Authorization;

namespace Bytestrone.AppraisalSystem.API.Endpoints.AppraisalForms;

public class GetFormQuestionsEndpoint(IMediator mediator) : Endpoint<GetFormQuestionsRequest, FormQuestionGroupedDTO>
{
    private readonly IMediator _mediator = mediator; 
    public override void Configure()
    {
        Get("/appraisal-forms/{EmployeeRoleId:int}/{EmployeeId:int}/questions");
        AllowAnonymous();
    }

    public override async Task HandleAsync(GetFormQuestionsRequest req, CancellationToken ct)
    {
        var query = new GetFormQuestionsQuery
        (
            req.EmployeeRoleId,
            req.EmployeeId
        );

        var result = await _mediator.Send(query, ct);

        if (result.IsSuccess)
        {
            await SendAsync(result.Value, cancellation: ct);
        }
        else if (result.Status == Ardalis.Result.ResultStatus.NotFound)
        {
            await SendNotFoundAsync(cancellation: ct);
        }
        else
        {
            await SendErrorsAsync(cancellation: ct);
        }
    }
}
