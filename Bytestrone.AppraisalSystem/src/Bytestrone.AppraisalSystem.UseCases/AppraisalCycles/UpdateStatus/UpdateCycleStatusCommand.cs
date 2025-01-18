using Ardalis.Result;
using Ardalis.SharedKernel;

namespace Bytestrone.AppraisalSystem.UseCases.AppraisalCycles.UpdateStatus;
public record UpdateCycleStatusCommand(int Id):ICommand<Result<int>>;
