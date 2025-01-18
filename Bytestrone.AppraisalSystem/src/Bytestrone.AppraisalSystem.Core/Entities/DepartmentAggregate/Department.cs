using Ardalis.SharedKernel;
using Bytestrone.AppraisalSystem.Core.Entities.EmployeeRoleAggregate;
using Bytestrone.AppraisalSystem.Core.Entities.PerformanceFactorAggregate;

namespace Bytestrone.AppraisalSystem.Core.Entities.DepartmentAggregate;
public class Department(string departmentName) : EntityBase, IAggregateRoot
{
    public string DepartmentName { get; private set; } = departmentName;

    public ICollection<DepartmentPerformanceFactor> DepartmentPerformanceFactors { get; set; } = new List<DepartmentPerformanceFactor>();

}