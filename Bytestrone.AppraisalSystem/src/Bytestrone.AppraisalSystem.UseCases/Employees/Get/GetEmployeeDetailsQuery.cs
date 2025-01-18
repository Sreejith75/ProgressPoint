using Ardalis.Result;
using Ardalis.SharedKernel;

namespace Bytestrone.AppraisalSystem.UseCases.Employees.Get;
public record GetEmployeeDetailsQuery(int EmployeeId):IQuery<Result<EmployeeDetailsDTO>>;