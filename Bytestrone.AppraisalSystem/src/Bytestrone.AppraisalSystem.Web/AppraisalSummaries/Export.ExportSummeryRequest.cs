namespace Bytestrone.AppraisalSystem.web.AppraisalSummaries;
public class ExportSummeryRequest
{
    public int Year { get; set; }
    public int? Quarter { get; set; }
    public int? RoleId { get; set; }
    public int? DepartmentId { get; set; }
}