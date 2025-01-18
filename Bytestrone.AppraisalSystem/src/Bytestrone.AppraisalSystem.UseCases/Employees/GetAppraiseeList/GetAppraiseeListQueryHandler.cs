using Ardalis.Result;
using Ardalis.SharedKernel;
using Bytestrone.AppraisalSystem.Core.EmployeeAggregate;
using Bytestrone.AppraisalSystem.Core.Entities.AppraisalFeedbackAggregate;
using Bytestrone.AppraisalSystem.Core.Entities.AppraisalFeedbackAggregate.Specification;
using Bytestrone.AppraisalSystem.Core.Entities.EmployeeAggregate.Specification;

namespace Bytestrone.AppraisalSystem.UseCases.Employees.GetAppraiseeList;

public class GetAppraiseeList(IRepository<Employee> employeeRepository, IRepository<AppraisalFeedback> appraisalFeedbackRepository) : IQueryHandler<GetAppraiseeListQuery, Result<List<AppraiseeDetailsDTO>>>
{
    private readonly IRepository<Employee> _employeeRepository = employeeRepository;
    private readonly IRepository<AppraisalFeedback> _appraisalFeedbackRepository = appraisalFeedbackRepository;

    public async Task<Result<List<AppraiseeDetailsDTO>>> Handle(GetAppraiseeListQuery request, CancellationToken cancellationToken)
    {
        var appraiserSpec = new EmployeeByIdSpec(request.AppraiserId);
        var appraiser = await _employeeRepository.FirstOrDefaultAsync(appraiserSpec, cancellationToken);
        if (appraiser == null)
        {
            return Result<List<AppraiseeDetailsDTO>>.Error($"Appraiser with ID {request.AppraiserId} not found.");
        }

        var appraiseeIds = appraiser.GetAppraiseeList();
        if (appraiseeIds == null || appraiseeIds.Count == 0) 
        {
            return Result<List<AppraiseeDetailsDTO>>.Success(new List<AppraiseeDetailsDTO>());
        }

        var appraisees = await _employeeRepository.ListAsync(new EmployeeByIdsSpec(appraiseeIds), cancellationToken);
        var appraisalFeedbacks = await _appraisalFeedbackRepository.ListAsync(
            new AppraisalFeedbackByEmployeeIdsSpec(appraiseeIds),
            cancellationToken
        );

        var appraiseeDetails = appraisees.Select(appraisee =>
        {
            var feedback = appraisalFeedbacks.FirstOrDefault(af => af.EmployeeId == appraisee.Id);
            return new AppraiseeDetailsDTO
            {
                AppraiseeId=appraisee.Id,
                Name = appraisee.GetFullName(),
                EmployeeRole = appraisee.Role?.RoleName ?? "Unknown",
                Feedbackstatus = feedback?.Status?.Name ?? "Not Started",
                AppraiseeScore = feedback?.AppraisalSummary?.AppraiseeScore ?? 0, 
                AppraiserScore=feedback?.AppraisalSummary?.AppraiserScore??0,
                PerformanceBucket=feedback?.AppraisalSummary?.PerformanceBucket.Name??"Unknown"

            };
        }).ToList();

        return Result<List<AppraiseeDetailsDTO>>.Success(appraiseeDetails);
    }

}
