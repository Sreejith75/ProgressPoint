using Ardalis.Result;
using Ardalis.SharedKernel;

namespace Bytestrone.AppraisalSystem.UseCases.AppraisalCycles.ListActive;
public record ListActiveCyclesQuery(): IQuery<Result<IEnumerable<ActiveCycleDTO>>>;