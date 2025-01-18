using Ardalis.Result;
using Ardalis.SharedKernel;
using Bytestrone.AppraisalSystem.Core.Entities.EmployeeRoleAggregate;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Bytestrone.AppraisalSystem.UseCases.EmployeeRoles.List;
public class ListEmployeeRolesQueryHandler(IRepository<EmployeeRole> repository) : IQueryHandler<ListEmployeeRolesQuery, Result<IEnumerable<EmployeeRoleDTO>>>
{
    private readonly IRepository<EmployeeRole> _repository = repository;

    public async Task<Result<IEnumerable<EmployeeRoleDTO>>> Handle(ListEmployeeRolesQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var roles = await _repository.ListAsync(cancellationToken);

            var roleDTOs = roles.Select(role => new EmployeeRoleDTO
            {
                Id = role.Id,
                RoleName = role.RoleName,
                HierarchyLevel = role.HierarchyLevel,
                DepartmentId = role.DepartmentId
            });

            return Result.Success(roleDTOs);
        }
        catch (Exception ex)
        {
            return Result.Error(ex.Message);
        }
    }
}
