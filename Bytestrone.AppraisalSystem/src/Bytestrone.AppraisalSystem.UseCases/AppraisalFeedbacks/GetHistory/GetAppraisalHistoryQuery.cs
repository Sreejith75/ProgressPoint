using Ardalis.Result;
using Ardalis.SharedKernel;

namespace Bytestrone.AppraisalSystem.UseCases.AppraisalFeedbacks.GetHistory;
public record GetAppraisalHistoryQuery(int EmployeeId):IQuery<Result<List<FeedbackHistoryDTO>>>;