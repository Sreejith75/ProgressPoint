using Ardalis.Specification;
using Bytestrone.AppraisalSystem.Core.EmployeeAggregate;

namespace Bytestrone.AppraisalSystem.Core.Entities.EmployeeAggregate.Specification;
public class EmployeeByIdsSpec : Specification<Employee>
{
    public EmployeeByIdsSpec(List<int> ids)
    {
        if (ids != null && ids.Count>0)
        {
            Query.Include(employee=>employee.Role).Where(employee => ids.Contains(employee.Id));
        }
    }
}