using Ardalis.Result;
using Ardalis.SharedKernel;
using Bytestrone.AppraisalSystem.Core.Entities.EmployeeRoleAggregate;

namespace Bytestrone.AppraisalSystem.UseCases.EmployeeRoles.Create;
public class CreateEmployeeRoleHandler(IRepository<EmployeeRole> _repository) : ICommandHandler<CreateEmployeeRoleCommand, Result<int>>
{
  public async Task<Result<int>> Handle(CreateEmployeeRoleCommand request, CancellationToken cancellationToken)
  {
    var newEmployeeRole= new EmployeeRole(request.RoleName,request.EmployeeRoleCode,request.HierarchyLevel ,request.DepartmentId);
    var createdEmployeeRole =await _repository.AddAsync(newEmployeeRole,cancellationToken);
    return createdEmployeeRole.Id;
  }
}