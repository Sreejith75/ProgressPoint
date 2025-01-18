using Ardalis.Specification;

namespace Bytestrone.AppraisalSystem.Core.Entities.PerformanceIndicatorAggregate.Specification;
public class GetIndicatorByIdSpec : Specification<PerformanceIndicator>
{
    public GetIndicatorByIdSpec(int Id) => Query.Where(i=>i.Id==Id).Include(i=>i.PerformanceFactor);
}