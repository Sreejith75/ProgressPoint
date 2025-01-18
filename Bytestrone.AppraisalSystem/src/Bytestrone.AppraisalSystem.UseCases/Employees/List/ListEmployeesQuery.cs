using Ardalis.Result;
using Ardalis.SharedKernel;

namespace Bytestrone.AppraisalSystem.UseCases.Employees.List;
public record ListEmployeeQuery():IQuery<Result<List<EmployeeDetailsDisplayDTO>>>;