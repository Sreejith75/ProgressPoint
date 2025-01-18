using Ardalis.Result;
using Ardalis.SharedKernel;

namespace Bytestrone.AppraisalSystem.UseCases.PerformanceFactors.ListFactorsForRoles;
public record ListFactorsForDepartmentsQuery():IQuery<Result<List<FactorsWithDepartmentsDTO>>>;