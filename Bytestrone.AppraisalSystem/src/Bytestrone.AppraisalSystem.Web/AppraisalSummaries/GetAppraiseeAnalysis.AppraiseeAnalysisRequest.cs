namespace Bytestrone.AppraisalSystem.web.AppraisalSummaries;
public class AppraiseeFilterRequest
{
    public int Year { get; set; }
    public int? Quarter { get; set; }
    public int? RoleId { get; set; }
    public int? DepartmentId { get; set; }
}