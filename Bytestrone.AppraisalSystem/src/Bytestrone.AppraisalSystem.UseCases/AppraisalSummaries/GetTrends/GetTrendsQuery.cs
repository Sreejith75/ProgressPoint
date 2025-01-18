using Ardalis.Result;
using Ardalis.SharedKernel;

namespace Bytestrone.AppraisalSystem.UseCases.AppraisalSummaries.GetTrends;
public record GetTrendsQuery():IQuery<Result<List<TrendsDTO>>>;