
using Bytestrone.AppraisalSystem.UseCases.AppraisalForms.List;
using Bytestrone.AppraisalSystem.web.AppraisalForms;
using FastEndpoints;
using MediatR;

namespace Bytestrone.AppraisalSystem.Web.AppraisalForms;

public class ListAppraisalForms(IMediator mediator) : EndpointWithoutRequest<IEnumerable<ListFormResponse>>
{
    public override void Configure()
    {
        Get("/AppraisalForms");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var result = await mediator.Send(new ListFormsQuery(), ct);

        if (result.IsSuccess)
        {
            var responseList = result.Value.Select(form => new ListFormResponse
            {
                Id = form.Id,
                EmployeeRole = form.EmployeeRole,
                Status = form.Status!.Name
            });

            await SendAsync(responseList, cancellation: ct);
        }
        else
        {
            await SendNotFoundAsync(ct);
        }
    }
}
