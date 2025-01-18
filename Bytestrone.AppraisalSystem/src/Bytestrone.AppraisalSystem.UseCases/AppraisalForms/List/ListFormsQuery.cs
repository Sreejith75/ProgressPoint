using Ardalis.Result;
using Ardalis.SharedKernel;

namespace Bytestrone.AppraisalSystem.UseCases.AppraisalForms.List;
public record ListFormsQuery():IQuery<Result<IEnumerable<AppraisalFormDTO>>>;
