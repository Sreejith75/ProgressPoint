using Ardalis.Result;
using Ardalis.SharedKernel;
using Bytestrone.AppraisalSystem.UseCases.Employees;

namespace Bytestrone.AppraisalSystem.UseCases.AppraisalFeedbacks.GetAppraiseeFeedbacks;
public record GetAppraiseeFeedbacksQuery(int AppraiseeId,int CycleId):IQuery<Result<AppraiseeFeedbackDetailDTO>>;