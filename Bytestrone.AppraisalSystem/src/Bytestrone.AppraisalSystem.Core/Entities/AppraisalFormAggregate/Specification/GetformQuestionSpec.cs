using Ardalis.Specification;

namespace Bytestrone.AppraisalSystem.Core.Entities.AppraisalFormAggregate.Specification;
public class GetFormQuestionsSpec : Specification<AppraisalForm>
{
    public GetFormQuestionsSpec( int employeeRoleId, FormStatus status)
    {
        Query.Where(f => f.EmployeeRoleId == employeeRoleId && f.Status==status)
             .Include(f => f.FormQuestionMappings)
             .ThenInclude(fq => fq.Question);
    }
}