using Bytestrone.AppraisalSystem.Core.Entities.AppraisalFormAggregate;

namespace Bytestrone.AppraisalSystem.UseCases.AppraisalForms;

public class AppraisalFormDTO
{
    public int Id { get; set; }
    public string Quarter { get; set; }= string.Empty;
    public int Year { get; set; }
    public string EmployeeRole { get; set; }= string.Empty;
    public FormStatus? Status { get; set; }
}
