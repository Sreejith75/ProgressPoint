using Ardalis.Result;
using Ardalis.SharedKernel;
using Bytestrone.AppraisalSystem.Core.Entities.AppraisalFormAggregate;
using Bytestrone.AppraisalSystem.Core.Entities.AppraisalFormAggregate.Specification;
using Bytestrone.AppraisalSystem.UseCases.AppraisalForms;
using Bytestrone.AppraisalSystem.UseCases.AppraisalForms.List;

namespace Bytestrone.AppraisalSystem.UseCases.AppraisalForms.List;

public class ListFormsHandler(IRepository<AppraisalForm> repository) : IQueryHandler<ListFormsQuery, Result<IEnumerable<AppraisalFormDTO>>>
{
    public async Task<Result<IEnumerable<AppraisalFormDTO>>> Handle(ListFormsQuery request, CancellationToken cancellationToken)
    {
        var spec = new AppraisalFormWithDetailsSpec();
        var result = await repository.ListAsync(spec, cancellationToken);

        if (!result.Any())
        {
            return Result.NotFound("No forms found.");
        }

        var formList = result.Select(x => new AppraisalFormDTO
        {
            Id = x.Id,
            EmployeeRole = x.EmployeeRole?.RoleName ?? "Unknown Role",
            Status = x.Status,
        });

        return Result.Success(formList);
    }
}
