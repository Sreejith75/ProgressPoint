using Ardalis.GuardClauses;
using Ardalis.SharedKernel;
using Bytestrone.AppraisalSystem.Core.EmployeeAggregate;
using Bytestrone.AppraisalSystem.Core.Entities.DepartmentAggregate;

namespace Bytestrone.AppraisalSystem.Core.Entities.EmployeeRoleAggregate;
public class EmployeeRole(string roleName, string employeeRoleCode, int hierarchyLevel, int departmentId) : EntityBase, IAggregateRoot
{
  public string RoleName { get; private set; } = Guard.Against.NullOrEmpty(roleName, nameof(roleName));
  public string EmployeeRoleCode { get; private set; } = Guard.Against.NullOrEmpty(employeeRoleCode, nameof(employeeRoleCode));
  public int HierarchyLevel { get; private set; } = Guard.Against.OutOfRange(hierarchyLevel, nameof(hierarchyLevel), 1, int.MaxValue);
  public int DepartmentId { get; private set; } = departmentId;
  public Department? Department { get; set; }
}