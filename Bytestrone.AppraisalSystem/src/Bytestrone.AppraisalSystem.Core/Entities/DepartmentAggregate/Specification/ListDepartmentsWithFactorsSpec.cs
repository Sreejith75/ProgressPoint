using Ardalis.Specification;

namespace Bytestrone.AppraisalSystem.Core.Entities.DepartmentAggregate.Specification;
public class ListDepartmentsWithFactorsSpec:Specification<Department>
{
    public ListDepartmentsWithFactorsSpec()
    {
        Query.Include(d=>d.DepartmentPerformanceFactors).ThenInclude(df=>df.PerformanceFactor);
    }
}