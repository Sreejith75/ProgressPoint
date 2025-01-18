using Ardalis.Specification;
namespace Bytestrone.AppraisalSystem.Core.Entities.PerformanceIndicatorAggregate.Specification;
public class GetIndicatorsByIdsSpec : Specification<PerformanceIndicator>
{
    public GetIndicatorsByIdsSpec(IEnumerable<int> ids)
    {
        Query.Include(i=>i.PerformanceFactor).Where(indicator => ids.Contains(indicator.Id));
    }
}
