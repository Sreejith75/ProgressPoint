using Ardalis.Result;
using Ardalis.SharedKernel;
using Bytestrone.AppraisalSystem.Core.SystemRoleAggregate;

namespace Bytestrone.AppraisalSystem.UseCases.SystemRoles.Delete;

public class DeleteSystemRoleHandler(IRepository<SystemRole> _repository)
  : ICommandHandler<DeleteSystemRoleCommand, Result>
{

    public async Task<Result> Handle(DeleteSystemRoleCommand request, CancellationToken cancellationToken)
    {
        var aggregateToDelete = await _repository.GetByIdAsync(request.SystemRoleId, cancellationToken);
        if (aggregateToDelete == null) return Result.NotFound();
        await _repository.DeleteAsync(aggregateToDelete,cancellationToken);
        return Result.Success();
    }
}
