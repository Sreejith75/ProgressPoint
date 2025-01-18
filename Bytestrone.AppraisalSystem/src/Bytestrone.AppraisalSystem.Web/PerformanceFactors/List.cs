using Ardalis.Result;
using Bytestrone.AppraisalSystem.UseCases.PerformanceFactors;
using Bytestrone.AppraisalSystem.UseCases.PerformanceFactors.List;
using FastEndpoints;
using MediatR;

namespace Bytestrone.AppraisalSystem.Web.PerformanceFactors;
public class List(IMediator mediator) : EndpointWithoutRequest<List<PerformanceFactorWithIndicatorsDTO>>
{
    private readonly IMediator _mediator = mediator;

  public override void Configure()
    {
        Get("/PerfomanceFactors");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CancellationToken cancellationToken)
    {
        Result<IEnumerable<PerformanceFactorWithIndicatorsDTO>> result = await _mediator.Send(new ListPerformancefactorsQuery(), cancellationToken);

        if (result.IsSuccess)
        {
            Response = result.Value.ToList();
        }
        else
        {
            Result.Error("Factors is not found");
        }
    }
}
