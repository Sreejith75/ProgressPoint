using Ardalis.Specification;
using Bytestrone.AppraisalSystem.Core.EmployeeAggregate;

namespace Bytestrone.AppraisalSystem.Core.Entities.EmployeeAggregate.Specification;
public class GetEmployeeByEmailSpec : Specification<Employee>
{
    public GetEmployeeByEmailSpec(string email) => Query.Where(e => e.Email == email);
}