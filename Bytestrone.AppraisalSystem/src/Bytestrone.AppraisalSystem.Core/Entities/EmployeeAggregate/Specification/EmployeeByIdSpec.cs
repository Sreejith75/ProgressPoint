using Ardalis.Specification;
using Bytestrone.AppraisalSystem.Core.EmployeeAggregate;

namespace Bytestrone.AppraisalSystem.Core.Entities.EmployeeAggregate.Specification;
public class EmployeeByIdSpec : Specification<Employee>
{
    public EmployeeByIdSpec(int Id) => Query
    .Where(s => s.Id == Id)
    .Include(s => s.Appraisees)
    .Include(s=>s.Appraisers)
    .Include(s=>s.Role)
    .ThenInclude(s=>s!.Department);
}