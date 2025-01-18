using Ardalis.Result;
using Ardalis.SharedKernel;
using Bytestrone.AppraisalSystem.Core.Entities.DepartmentAggregate;
using Bytestrone.AppraisalSystem.Core.Entities.DepartmentAggregate.Specification;

namespace Bytestrone.AppraisalSystem.UseCases.PerformanceFactors.ListFactorsForRoles;

public class ListFactorsForRolesQueryHandler(IRepository<Department> departmentRepository) : IQueryHandler<ListFactorsForDepartmentsQuery, Result<List<FactorsWithDepartmentsDTO>>>
{
    private readonly IRepository<Department> _departmentRepository = departmentRepository;

  public async Task<Result<List<FactorsWithDepartmentsDTO>>> Handle(ListFactorsForDepartmentsQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var departmentsSpec = new ListDepartmentsWithFactorsSpec();
            var departments = await _departmentRepository.ListAsync(departmentsSpec, cancellationToken);

            if (departments == null || !departments.Any())
            {
                return Result<List<FactorsWithDepartmentsDTO>>.NotFound();
            }

            var result = departments.Select(department => new FactorsWithDepartmentsDTO
            {
                DepartmentId = department.Id,
                DepartmentName = department.DepartmentName,
                factors = department.DepartmentPerformanceFactors.Select(factor => new FactorDTO
                {
                    FactorId = factor.Id,
                    FactorName = factor.PerformanceFactor.FactorName,
                    Weightage = factor.Weightage
                }).ToList()
            }).ToList();

            return Result<List<FactorsWithDepartmentsDTO>>.Success(result);
        }
        catch (Exception ex)
        {
            return Result<List<FactorsWithDepartmentsDTO>>.Error($"An error occurred while fetching factors: {ex.Message}");
        }
    }
}
