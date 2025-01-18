using Ardalis.Result;
using Ardalis.SharedKernel;

namespace Bytestrone.AppraisalSystem.UseCases.EmployeeRoles.List;
public record ListEmployeeRolesQuery : IQuery<Result<IEnumerable<EmployeeRoleDTO>>>;
