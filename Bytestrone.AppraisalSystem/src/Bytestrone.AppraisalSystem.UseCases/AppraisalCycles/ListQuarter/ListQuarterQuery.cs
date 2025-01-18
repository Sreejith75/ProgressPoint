using Ardalis.Result;
using Ardalis.SharedKernel;

namespace Bytestrone.AppraisalSystem.UseCases.AppraisalCycles.ListQuarter;
public record ListQuarterQuery():IQuery<Result<List<QuarterDTO>>>;