namespace Bytestrone.AppraisalSystem.web.AppraisalForms;
public record CreateAppraisalFormRequest
{
    public int EmployeeRoleId { get; set; } 
    public List<int>? QuestionIds { get; set; }
}