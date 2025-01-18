using Ardalis.Specification;
using System.Linq.Expressions;

namespace Bytestrone.AppraisalSystem.Core.Entities.EmployeeRoleAggregate.Specification;
public class EmployeeRoleByIdSpec : Specification<EmployeeRole>
{
    public EmployeeRoleByIdSpec(int id)
    {
        Query.Where(role => role.Id == id).Include(role=>role.Department);
    }
}
