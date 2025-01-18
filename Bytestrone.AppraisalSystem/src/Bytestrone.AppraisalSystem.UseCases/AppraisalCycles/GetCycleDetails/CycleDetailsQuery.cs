using Ardalis.Result;
using Ardalis.SharedKernel;

namespace Bytestrone.AppraisalSystem.UseCases.AppraisalCycles.GetCycleDetails;
public record CycleDetailsQuery():IQuery<Result<CycleDetailsDTO>>;