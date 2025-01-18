using Ardalis.Result;
using Ardalis.SharedKernel;

namespace Bytestrone.AppraisalSystem.UseCases.AppraisalFeedbacks.Get;
public record AppraisalFeedbackQuery(int EmployeeId, int CycleId) : IQuery<Result<AppraisalFeedbackDTO>>;