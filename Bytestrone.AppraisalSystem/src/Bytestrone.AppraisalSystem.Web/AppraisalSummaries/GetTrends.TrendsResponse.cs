using Bytestrone.AppraisalSystem.UseCases.AppraisalSummaries.GetTrends;

namespace Bytestrone.AppraisalSystem.web.AppraisalSummaries;
public class TrendsResponse
{
    public string? Message { get; set; }
    public List<TrendsDTO>? trends { get; set; }
}