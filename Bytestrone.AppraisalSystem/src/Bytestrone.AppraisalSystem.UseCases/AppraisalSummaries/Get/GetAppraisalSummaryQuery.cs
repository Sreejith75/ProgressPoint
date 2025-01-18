using Ardalis.Result;
using Ardalis.SharedKernel;

namespace Bytestrone.AppraisalSystem.UseCases.AppraisalSummaries.Get;
public record GetAppraisalSummeryQuery(int EmployeeId,int CycleId):IQuery<Result<AppraisalSummaryDTO>>;
