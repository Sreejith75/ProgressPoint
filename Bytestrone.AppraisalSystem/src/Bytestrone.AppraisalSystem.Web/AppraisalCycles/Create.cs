using FastEndpoints;
using Bytestrone.AppraisalSystem.web.AppraisalCycles;
using MediatR;
using Bytestrone.AppraisalSystem.UseCases.AppraisalCycles.Create;

namespace Bytestrone.AppraisalSystem.Api.Endpoints.AppraisalCycles;
public class Create(IMediator _mediator) : Endpoint<CreateAppraisalCycleRequest, CreateAppraisalCycleResponse>
{


    public override void Configure()
    {
        Post("/appraisal-cycles");
        Summary(s =>
        {
            s.ExampleRequest = new CreateAppraisalCycleRequest
            {
                Quarter = 1,
                Year = 2024,
                AppraiseeStartDate = DateOnly.MinValue,
                AppraiseeEndDate = DateOnly.MaxValue,
                AppraiserStartDate = DateOnly.MinValue,
                AppraiserEndDate = DateOnly.MaxValue,

            };
        });
    }

    public override async Task HandleAsync(CreateAppraisalCycleRequest req, CancellationToken ct)
    {
        var command = new CreateAppraisalCycleCommand(req.Quarter, req.Year, req.AppraiseeStartDate, req.AppraiseeEndDate, req.AppraiserStartDate, req.AppraiserEndDate);
        var result = await _mediator.Send(command, ct);

        if (result.IsSuccess)
        {
            Response = new CreateAppraisalCycleResponse(result.Value, "Cycle Created Successfully");
            return;
        }
    }


}