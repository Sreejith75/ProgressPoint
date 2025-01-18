using Bytestrone.AppraisalSystem.UseCases.AppraisalCycles.ListQuarterYear;

namespace Bytestrone.AppraisalSystem.web.AppraisalCycles;
public record ListQuarterYearResponse
{
    public string? Message { get; set; }
    public QuarterYearDTO? quarterYears { get; set; }
}