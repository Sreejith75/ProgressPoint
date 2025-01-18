using MediatR;
using FastEndpoints;
using Ardalis.Result;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using Bytestrone.AppraisalSystem.UseCases.AppraisalCycles.GetActive;
using Microsoft.AspNetCore.Authorization;

namespace Bytestrone.AppraisalSystem.web.AppraisalCycles;
public class GetActiveAppraisalCycleEndpoint(IMediator mediator) : EndpointWithoutRequest<AppraisalCycleRecord>
{
    private readonly IMediator _mediator = mediator;
    public override void Configure()
    {
        Get("/appraisal-cycles/active");  
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var result = await _mediator.Send(new GetActiveAppraisalCycleQuery(), ct);

        if (result.IsSuccess)
        {
            var cycleDto = result.Value;
            var record = new AppraisalCycleRecord
            {
                Id = cycleDto.Id,
                Quarter = cycleDto.Quarter,
                Year = cycleDto.Year,
                AppraiseeStartDate = cycleDto.AppraiseeStartDate,
                AppraiseeEndDate = cycleDto.AppraiseeEndDate,
                AppraiserStartDate = cycleDto.AppraiserStartDate,
                AppraiserEndDate = cycleDto.AppraiserEndDate,
                Status = cycleDto.Status
            };

            await SendAsync(record, 200, ct);
        }
        else
        {
            await SendErrorsAsync(cancellation: ct);
        }
    }
}
