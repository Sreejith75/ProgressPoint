using Ardalis.Result;

namespace Bytestrone.AppraisalSystem.UseCases.EmployeeRoles.Create;
public record CreateEmployeeRoleCommand(string RoleName, string EmployeeRoleCode,int HierarchyLevel, int DepartmentId ):Ardalis.SharedKernel.ICommand<Result<int>>;