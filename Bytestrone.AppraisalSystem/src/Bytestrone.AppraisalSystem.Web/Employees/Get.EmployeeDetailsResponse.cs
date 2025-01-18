using Microsoft.AspNetCore.Identity;

namespace Bytestrone.AppraisalSystem.web.Employees;
public class EmployeeDetailsResponse
{
    public string? Name { get; set; }
    public string? Email { get; set; }
    public string? PhoneNumber { get; set; }
    public string? Role  { get; set; }
    public string? Department { get; set; }
    public string? Manager  { get; set; }
    public int AppraisalsCompleted { get; set; }
    public decimal? AverageAppraisalScore { get; set; }
    public string? PerformanceBucket { get; set; }
    public bool status { get; set; }
}