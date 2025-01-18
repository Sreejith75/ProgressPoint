using Bytestrone.AppraisalSystem.UseCases.AppraisalCycles.List;
using Bytestrone.AppraisalSystem.web.AppraisalCycles;
using FastEndpoints;
using MediatR;
using Microsoft.AspNetCore.Authorization;

namespace Bytestrone.AppraisalSystem.Web.AppraisalCycles;
public class ListAppraisalCyclesEndpoint(IMediator mediator) : EndpointWithoutRequest<IEnumerable<AppraisalCycleRecord>>
{
    private readonly IMediator _mediator = mediator;
    public override void Configure()
    {
        Get("/appraisal-cycles");
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var result = await _mediator.Send(new ListAppraisalCycleQuery(), ct);

        if (result.IsSuccess)
        {
            var records = result.Value.Select(dto => new AppraisalCycleRecord
            {
                Id = dto.Id,
                Quarter = dto.Quarter,
                Year = dto.Year,
                AppraiseeStartDate = dto.AppraiseeStartDate,
                AppraiseeEndDate = dto.AppraiseeEndDate,
                AppraiserStartDate = dto.AppraiserStartDate,
                AppraiserEndDate = dto.AppraiserEndDate,
                Status = dto.Status,
            });

            await SendAsync(records, cancellation: ct);
        }
    }
}
