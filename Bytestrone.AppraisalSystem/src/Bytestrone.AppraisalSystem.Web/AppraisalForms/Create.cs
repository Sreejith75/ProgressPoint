using FastEndpoints;
using MediatR;
using System.Threading.Tasks;
using System.Threading;
using Bytestrone.AppraisalSystem.UseCases.AppraisalForms.Create;

namespace Bytestrone.AppraisalSystem.web.AppraisalForms;
public class CreateAppraisalFormEndpoint(IMediator mediator) : Endpoint<CreateAppraisalFormRequest, CreateAppraisalFormResponse>
{
    private readonly IMediator _mediator = mediator;

  public override void Configure()
    {
        Post("/Appraisalforms");  
        AllowAnonymous();           
    }

    public override async Task HandleAsync(CreateAppraisalFormRequest req, CancellationToken ct)
    {
        var command = new CreateAppraisalFormCommand(req.EmployeeRoleId,req.QuestionIds!);
        var result = await _mediator.Send(command, ct);  

        if (result.IsSuccess)
        {
            Response= new CreateAppraisalFormResponse(result.Value,"Appraisal form created successfully");
            await SendAsync(Response,cancellation:ct); 
        }
        else
        {
            await SendErrorsAsync(cancellation:ct);
        }
    }
}
