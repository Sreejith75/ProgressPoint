using Ardalis.Result;
using Ardalis.SharedKernel;

namespace Bytestrone.AppraisalSystem.UseCases.AppraisalCycles.List;
public record ListAppraisalCycleQuery(): IQuery<Result<IEnumerable<AppraisalCycleDTO>>>;