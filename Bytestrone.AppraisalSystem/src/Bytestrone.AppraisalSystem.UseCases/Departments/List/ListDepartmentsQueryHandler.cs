using Ardalis.Result;
using Ardalis.SharedKernel;
using Bytestrone.AppraisalSystem.Core.Entities.DepartmentAggregate;

namespace Bytestrone.AppraisalSystem.UseCases.Departments.List;
public class ListDepartmentsQueryHandler(IRepository<Department> departmentRepository) : IQueryHandler<ListDepartmentsQuery, Result<List<DepartmentDTO>>>
{
    private readonly IRepository<Department> _departmentRepository = departmentRepository;

  public async Task<Result<List<DepartmentDTO>>> Handle(ListDepartmentsQuery request, CancellationToken cancellationToken)
    {
        var departments = await _departmentRepository.ListAsync(cancellationToken);

        if (departments == null || !departments.Any())
        {
            return Result<List<DepartmentDTO>>.Error("No departments found.");
        }

        var departmentDtos = departments.Select(department => new DepartmentDTO
        {
            DepartmentId = department.Id,  
            Name = department.DepartmentName
        }).ToList();

        return Result<List<DepartmentDTO>>.Success(departmentDtos);
    }
}