using Bytestrone.AppraisalSystem.UseCases.Employees;

namespace Bytestrone.AppraisalSystem.web.Employees;
public class AppraiseeGetResponse
{
    public string? Message { get; set; }
    public List<AppraiseeDetailsDTO>? AppraiseeList { get; set; }
}