using Ardalis.Result;
using Ardalis.SharedKernel;
using Bytestrone.AppraisalSystem.Core.EmployeeAggregate;
using Bytestrone.AppraisalSystem.Core.Entities.EmployeeAggregate;
using Bytestrone.AppraisalSystem.Core.Entities.EmployeeAggregate.Specification;
using Bytestrone.AppraisalSystem.Core.Entities.EmployeeRoleAggregate;
using Bytestrone.AppraisalSystem.Core.Entities.EmployeeRoleAggregate.Specification;
using Bytestrone.AppraisalSystem.Core.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Bytestrone.AppraisalSystem.UseCases.Employees.GetAppraisersList;

public class GetAppraisersQueryHandler(IRepository<Employee> employeeRepository, IRepository<EmployeeRole> employeeRoleRepository) : IQueryHandler<GetAppraisersQuery, Result<List<AppraisersDTO>>>
{
    private readonly IRepository<Employee> _employeeRepository = employeeRepository;
    private readonly IRepository<EmployeeRole> _employeeRoleRepository = employeeRoleRepository;

    public async Task<Result<List<AppraisersDTO>>> Handle(GetAppraisersQuery request, CancellationToken cancellationToken)
    {
        var employeeRoleSpec = new EmployeeRoleByIdSpec(request.RoleId);
        var employeeRole = await _employeeRoleRepository.FirstOrDefaultAsync(employeeRoleSpec, cancellationToken);

        if (employeeRole == null)
        {
            return Result<List<AppraisersDTO>>.Error("Employee role not found.");
        }

        var employeeSpec = new EmployeeByRoleIdSpec(employeeRole.DepartmentId, employeeRole.HierarchyLevel);
        var employees = await _employeeRepository.ListAsync(employeeSpec, cancellationToken);

        if (employees.Count == 0)
        {
            return Result<List<AppraisersDTO>>.Error("No employees found with the specified criteria.");
        }

        var appraisers = employees
            .Select(e => new AppraisersDTO
            {
                Id = e.Id,
                Name = e.GetFullName()
            })
            .ToList();

        if (!appraisers.Any())
        {
            return Result<List<AppraisersDTO>>.Error("No appraisers found with a higher hierarchy level.");
        }

        return Result<List<AppraisersDTO>>.Success(appraisers);
    }

}
