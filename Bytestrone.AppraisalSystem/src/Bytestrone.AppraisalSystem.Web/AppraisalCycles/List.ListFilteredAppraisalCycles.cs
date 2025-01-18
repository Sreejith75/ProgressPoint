using Bytestrone.AppraisalSystem.UseCases.AppraisalCycles.ListActive;
using Bytestrone.AppraisalSystem.web.AppraisalCycles;
using FastEndpoints;
using MediatR;

public class ListSimplifiedAppraisalCyclesEndpoint(IMediator mediator) : EndpointWithoutRequest<IEnumerable<SimplifiedAppraisalCycleRecord>>
{
    private readonly IMediator _mediator = mediator;

  public override void Configure()
    {
        Get("/appraisal-cycles/simple-cycle");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var result = await _mediator.Send(new ListActiveCyclesQuery(), ct);

        if (result.IsSuccess)
        {
            var records = result.Value.Select(dto => new SimplifiedAppraisalCycleRecord
            {
                Id = dto.Id,
                Quarter = dto.Quarter!,
                Year = dto.Year
            });

            await SendAsync(records, cancellation: ct);
        }
    }
}
