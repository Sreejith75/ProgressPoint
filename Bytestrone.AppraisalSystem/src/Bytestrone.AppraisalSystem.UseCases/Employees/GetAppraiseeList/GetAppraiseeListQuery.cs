using Ardalis.Result;
using Ardalis.SharedKernel;

namespace Bytestrone.AppraisalSystem.UseCases.Employees.GetAppraiseeList;
public record GetAppraiseeListQuery(int AppraiserId):IQuery<Result<List<AppraiseeDetailsDTO>>>;