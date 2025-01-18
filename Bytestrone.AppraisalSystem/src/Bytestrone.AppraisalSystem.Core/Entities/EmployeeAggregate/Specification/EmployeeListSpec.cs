using System.Security.Cryptography.X509Certificates;
using Ardalis.Specification;
using Bytestrone.AppraisalSystem.Core.EmployeeAggregate;

namespace Bytestrone.AppraisalSystem.Core.Entities.EmployeeAggregate.Specification;
public class EmployeeListSpec:Specification<Employee>
{
    public EmployeeListSpec()
    {
        Query.Include(e=>e.Appraisers).Include(e=>e.Appraisees).Include(e=>e.Role).ThenInclude(r=>r!.Department).Include(e=>e.SystemRoles);
    }
}