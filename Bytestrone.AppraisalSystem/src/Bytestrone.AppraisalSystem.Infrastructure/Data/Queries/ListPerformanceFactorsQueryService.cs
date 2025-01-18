using Bytestrone.AppraisalSystem.UseCases.PerformanceFactors;
using Bytestrone.AppraisalSystem.UseCases.PerformanceFactors.List;
using Microsoft.EntityFrameworkCore;

namespace Bytestrone.AppraisalSystem.Infrastructure.Data.Queries;
public class ListPerformanceFactorsQueryService(AppDbContext _db) : IListPerformanceFactorsQueryService
{
    public async Task<IEnumerable<PerformanceFactorWithIndicatorsDTO>> ListAsync()
    {
        var factors = await _db.PerformanceFactors
                    .Include(factor => factor.Indicators)
                    .Select(factor => new PerformanceFactorWithIndicatorsDTO(
                        factor.Id,
                        factor.FactorName,
                        factor.Indicators.Select(indicator => new PerformanceIndicatorsDTO(indicator.Id, indicator.IndicatorName!)).ToList()
                    )).ToListAsync();
        return factors;
    }
}