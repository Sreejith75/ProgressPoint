using Ardalis.Specification;
using Bytestrone.AppraisalSystem.Core.EmployeeAggregate;

namespace Bytestrone.AppraisalSystem.Core.Entities.EmployeeAggregate.Specification;

public class EmployeeByRoleIdSpec : Specification<Employee>
{
    public EmployeeByRoleIdSpec(int departmentId, int currentHierarchyLevel)
    {
        Query
            .Include(x => x.Role) 
            .Where(e => e.Role!.DepartmentId == departmentId &&  
                        e.Role.HierarchyLevel > currentHierarchyLevel);
    }
}
