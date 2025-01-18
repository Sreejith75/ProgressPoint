using Ardalis.Result;
using Ardalis.SharedKernel;
using Bytestrone.AppraisalSystem.Core.Entities.PerformanceFactorAggregate;
using Bytestrone.AppraisalSystem.Core.Entities.PerformanceFactorAggregate.Specification;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Bytestrone.AppraisalSystem.UseCases.PerformanceFactors.ListIndicators;
public class ListIndicatorsQueryHandler : IQueryHandler<ListIndicatorsQuery, Result<List<FactorDTO>>>
{
    private readonly IRepository<PerformanceFactor> _factorRepository;

    public ListIndicatorsQueryHandler(IRepository<PerformanceFactor> factorRepository)
    {
        _factorRepository = factorRepository;
    }

    public async Task<Result<List<FactorDTO>>> Handle(ListIndicatorsQuery request, CancellationToken cancellationToken)
    {
        var factorSpec = new ListFactorsSpec();
        var factors = await _factorRepository.ListAsync(factorSpec, cancellationToken);

        if (factors == null || factors.Count == 0)
        {
            return Result<List<FactorDTO>>.Error("No performance factors found.");
        }

        var factorDTOs = new List<FactorDTO>();

        foreach (var factor in factors)
        {
            var factorDTO = new FactorDTO
            {
                FactorId = factor.Id,
                FactorName = factor.FactorName,
                Indicators = factor.Indicators != null && factor.Indicators.Any()
                    ? factor.Indicators.Select(indicator => new IndcatorDTO
                    {
                        IndicatorId = indicator.Id,
                        IndicatorName = indicator.IndicatorName,
                        Weightage = indicator.Weightage
                    }).ToList()
                    : new List<IndcatorDTO>()
            };

            factorDTOs.Add(factorDTO);
        }

        return Result<List<FactorDTO>>.Success(factorDTOs);
    }
}
