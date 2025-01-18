using Ardalis.Result;
using Ardalis.SharedKernel;

namespace Bytestrone.AppraisalSystem.UseCases.AppraisalCycles.GetActive;
public record GetActiveAppraisalCycleQuery():IQuery<Result<AppraisalCycleDTO>>;