using Bytestrone.AppraisalSystem.UseCases.Employees.GetAppraisersList;

namespace Bytestrone.AppraisalSystem.web.Employees;
public class GetAppraisersResponse
{
    public string? Message { get; set; }
    public List<AppraisersDTO>? appraisers { get; set; }
}