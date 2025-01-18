using Ardalis.Result;
using Ardalis.SharedKernel;
using Bytestrone.AppraisalSystem.Core.EmployeeAggregate;
using Bytestrone.AppraisalSystem.Core.Entities.AppraisalFeedbackAggregate;
using Bytestrone.AppraisalSystem.Core.Entities.AppraisalFeedbackAggregate.Specification;
using Bytestrone.AppraisalSystem.Core.Entities.AppraisalSummeryAggregate;
using Bytestrone.AppraisalSystem.Core.Entities.EmployeeAggregate.Specification;

namespace Bytestrone.AppraisalSystem.UseCases.Employees.Get;

public class GetEmployeeDetailsQueryHandler(IRepository<Employee> employeeRepository, IRepository<AppraisalFeedback> feedbackRepository) : IQueryHandler<GetEmployeeDetailsQuery, Result<EmployeeDetailsDTO>>
{
    private readonly IRepository<Employee> _employeeRepository = employeeRepository;
    private readonly IRepository<AppraisalFeedback> _feedbackRepository = feedbackRepository;

    public async Task<Result<EmployeeDetailsDTO>> Handle(GetEmployeeDetailsQuery request, CancellationToken cancellationToken)
    {
        // Fetch the employee using the specification
        var employeeSpec = new EmployeeByIdSpec(request.EmployeeId);
        var employee = await _employeeRepository.FirstOrDefaultAsync(employeeSpec, cancellationToken);

        // Handle case where employee is not found
        if (employee == null)
        {
            return Result<EmployeeDetailsDTO>.NotFound();
        }

        // Fetch feedbacks associated with the employee
        var feedbackSpec = new AppraisalFeedbackByEmployeeIdSpec(employee.Id);
        var feedbackList = await _feedbackRepository.ListAsync(feedbackSpec, cancellationToken);

        // Calculate appraisals completed and average score
        var appraisalsCompleted = feedbackList.Count;
        var averageAppraisalScore = feedbackList.Any()
            ? feedbackList.Average(f => f.AppraisalSummary?.AppraiserScore) 
            : 0;

        // Map employee entity to EmployeeDetailsDTO
        var employeeDetails = new EmployeeDetailsDTO
        {
            Name = employee.GetFullName(),
            Email = employee.Email,
            PhoneNumber = employee.PhoneNumber,
            Role = employee.Role?.RoleName,
            Department = employee.Role?.Department?.DepartmentName,
            Manager = employee.GetCurrentManagerName(), 
            AppraisalsCompleted = appraisalsCompleted,
            AverageAppraisalScore = averageAppraisalScore,
            PerformanceBucket = PerformanceBucket.GetBucketForScore(averageAppraisalScore).Name,
        };

        return Result<EmployeeDetailsDTO>.Success(employeeDetails);
    }
}
