using Ardalis.GuardClauses;
using Bytestrone.AppraisalSystem.Core.SystemRoleAggregate;


namespace Bytestrone.AppraisalSystem.Core.EmployeeAggregate;
public class EmployeeSystemRole(int employeeId, int systemRoleId)
{
  public int EmployeeId { get; set; } = employeeId;
  public virtual Employee Employee { get; set; }=default!;

  public int SystemRoleId { get; set; } = systemRoleId;
  public virtual SystemRole SystemRole { get; set; }=default!;
}
