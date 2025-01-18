using Ardalis.Specification;

namespace Bytestrone.AppraisalSystem.Core.Entities.AppraisalFormAggregate.Specification;

public class GetFormByCycleAndRoleIdSpec : Specification<AppraisalForm>
{
    public GetFormByCycleAndRoleIdSpec(int cycleId, int roleId, FormStatus status)
    {
        Query.Where(f =>
                         f.EmployeeRoleId == roleId && 
                         f.Status == status);
    }
}
