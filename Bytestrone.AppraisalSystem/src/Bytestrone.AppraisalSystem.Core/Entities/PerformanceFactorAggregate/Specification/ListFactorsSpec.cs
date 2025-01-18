using Ardalis.Specification;

namespace Bytestrone.AppraisalSystem.Core.Entities.PerformanceFactorAggregate.Specification;
public class ListFactorsSpec:Specification<PerformanceFactor>
{
    public ListFactorsSpec()
    {
        Query.Include(pf=>pf.Indicators);
    }
}