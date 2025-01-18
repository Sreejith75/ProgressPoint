using Ardalis.Result;
using Ardalis.SharedKernel;

namespace Bytestrone.AppraisalSystem.UseCases.Employees.GetAppraisersList;
public record GetAppraisersQuery(int RoleId):IQuery<Result<List<AppraisersDTO>>>;