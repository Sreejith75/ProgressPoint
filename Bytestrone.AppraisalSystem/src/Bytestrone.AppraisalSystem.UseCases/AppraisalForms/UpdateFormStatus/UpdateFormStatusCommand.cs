using Ardalis.Result;
using Ardalis.SharedKernel;

namespace Bytestrone.AppraisalSystem.UseCases.AppraisalForms.UpdateFormStatus;
public record UpdateFormStatusCommand(int Id): ICommand<Result<int>>;