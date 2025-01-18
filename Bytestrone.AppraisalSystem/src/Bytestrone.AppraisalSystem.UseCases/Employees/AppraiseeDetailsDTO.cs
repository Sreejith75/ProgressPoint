
namespace Bytestrone.AppraisalSystem.UseCases.Employees;
public record AppraiseeDetailsDTO
{
    public int AppraiseeId { get; set; }
    public string? Name { get; set; }
    public string? EmployeeRole { get; set; } 
    public string? Feedbackstatus { get; set; }
    public decimal AppraiseeScore { get; set; }
    public decimal AppraiserScore { get; set; }
    public string? PerformanceBucket { get; set; }
    
}