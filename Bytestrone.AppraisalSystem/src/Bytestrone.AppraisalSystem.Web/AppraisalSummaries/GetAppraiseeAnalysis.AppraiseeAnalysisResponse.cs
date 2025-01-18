using Bytestrone.AppraisalSystem.UseCases.AppraisalSummaries.AppraiseeAnalysis;

namespace Bytestrone.AppraisalSystem.web.AppraisalSummaries;
public class AppraiseeAnalysisResponse
{
    public string? Message { get; set; }
    public List<AppraiseeAnalysisDTO>? AppraiseeAnalysis { get; set; }
}