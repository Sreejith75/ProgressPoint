using Ardalis.Result;
using Ardalis.SharedKernel;

namespace Bytestrone.AppraisalSystem.UseCases.Departments.List;
public record ListDepartmentsQuery():IQuery<Result<List<DepartmentDTO>>>;