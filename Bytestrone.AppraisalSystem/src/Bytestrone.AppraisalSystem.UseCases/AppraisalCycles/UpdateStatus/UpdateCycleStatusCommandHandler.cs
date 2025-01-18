using Ardalis.Result;
using Ardalis.SharedKernel;
using Bytestrone.AppraisalSystem.Core.Entities.AppraisalCycleAggregate;

namespace Bytestrone.AppraisalSystem.UseCases.AppraisalCycles.UpdateStatus;
public class UpdateCycleStatusCommandHandler(IRepository<AppraisalCycle> repository) : ICommandHandler<UpdateCycleStatusCommand, Result<int>>
{
  private readonly IRepository<AppraisalCycle> _repository = repository;

  public async Task<Result<int>> Handle(UpdateCycleStatusCommand request, CancellationToken cancellationToken)
  {
    var cycle = await _repository.GetByIdAsync(request.Id, cancellationToken);
    if (cycle == null)
    {
      return Result.NotFound("Appraisal Cycle Not Found");
    }

    if (cycle.Status == CycleStatus.NotStarted)
    {
      cycle.StartCycle();
    }
    else if (cycle.Status == CycleStatus.InProgress)
    {
      cycle.CompleteCycle();
    }
    else
    {
      return Result.Invalid();
    }

    await _repository.UpdateAsync(cycle, cancellationToken);
    await _repository.SaveChangesAsync(cancellationToken);

    return Result.Success(cycle.Id);
  }
}
