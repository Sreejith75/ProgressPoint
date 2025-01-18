using Ardalis.Result;
using Ardalis.SharedKernel;
using Bytestrone.AppraisalSystem.Core.Entities.AppraisalFormAggregate;

namespace Bytestrone.AppraisalSystem.UseCases.AppraisalForms.UpdateFormStatus;
public class UpdateFormStatusCommandHandler(IRepository<AppraisalForm> repository) : ICommandHandler<UpdateFormStatusCommand, Result<int>>
{
    public async Task<Result<int>> Handle(UpdateFormStatusCommand request, CancellationToken cancellationToken)
    {
        var form = await repository.GetByIdAsync(request.Id, cancellationToken);
        if (form == null)
        {
            return Result<int>.NotFound("Appraisal form not found");
        }

        if (form.Status == FormStatus.Draft)
        {
            form.ActivateForm();
        }
        else if (form.Status == FormStatus.Active)
        {
            form.ArchiveForm();
        }
        else if(form.Status == FormStatus.Archived)
        {
            form.DraftForm();
        }
        else
        {
            return Result.Invalid();
        }

        await repository.UpdateAsync(form, cancellationToken);
        await repository.SaveChangesAsync(cancellationToken);

        return Result.Success(form.Id);
    }
}