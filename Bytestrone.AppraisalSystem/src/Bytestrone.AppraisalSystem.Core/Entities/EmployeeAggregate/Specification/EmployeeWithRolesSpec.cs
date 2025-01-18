using Ardalis.Specification;
using Bytestrone.AppraisalSystem.Core.EmployeeAggregate;

public class EmployeeWithRolesSpecification : Specification<Employee>
{
     public EmployeeWithRolesSpecification(string email)
    {
        Query.Where(e => e.Email == email)
             .Include(e => e.SystemRoles)
             .ThenInclude(sr => sr.SystemRole)
             .ThenInclude(sr => sr.SystemRolePermissions) 
             .ThenInclude(rp => rp.Permission);
    }
}
