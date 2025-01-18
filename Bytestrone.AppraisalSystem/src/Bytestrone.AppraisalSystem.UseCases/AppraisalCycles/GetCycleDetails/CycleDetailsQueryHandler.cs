using Ardalis.Result;
using Ardalis.SharedKernel;
using Bytestrone.AppraisalSystem.Core.EmployeeAggregate;
using Bytestrone.AppraisalSystem.Core.Entities.AppraisalCycleAggregate;
using Bytestrone.AppraisalSystem.Core.Entities.AppraisalCycleAggregate.Specification;
using Bytestrone.AppraisalSystem.Core.Entities.AppraisalFeedbackAggregate;
using Bytestrone.AppraisalSystem.Core.Entities.AppraisalFeedbackAggregate.Specification;
using Bytestrone.AppraisalSystem.Core.Entities.EmployeeAggregate.Specification;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Bytestrone.AppraisalSystem.UseCases.AppraisalCycles.GetCycleDetails;

public class GetCycleDetailsQueryHandler : IQueryHandler<CycleDetailsQuery, Result<CycleDetailsDTO>>
{
    private readonly IRepository<AppraisalCycle> _cycleRepository;
    private readonly IRepository<AppraisalFeedback> _feedbackRepository;
    private readonly IRepository<Employee> _employeeRepository;

    public GetCycleDetailsQueryHandler(
        IRepository<AppraisalCycle> cycleRepository,
        IRepository<AppraisalFeedback> feedbackRepository,
        IRepository<Employee> employeeRepository)
    {
        _cycleRepository = cycleRepository;
        _feedbackRepository = feedbackRepository;
        _employeeRepository = employeeRepository;
    }

    public async Task<Result<CycleDetailsDTO>> Handle(CycleDetailsQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var activeCycleSpec = new ActiveCycleSpec();
            var cycle = await _cycleRepository.FirstOrDefaultAsync(activeCycleSpec, cancellationToken);

            if (cycle == null)
            {
                return Result<CycleDetailsDTO>.NotFound("No active appraisal cycle found.");
            }

            var feedbackSpec = new AppraisalFeedbackByCycleIdSpec(cycle.Id);
            var feedbacks = await _feedbackRepository.ListAsync(feedbackSpec, cancellationToken);

            var employeeSpec = new EmployeeListSpec();
            var totalEmployees = await _employeeRepository.ListAsync(employeeSpec, cancellationToken);

            var completedCount = feedbacks.Count(f => f.Status == FeedbackStatus.Completed);
            var pendingCount = feedbacks.Count(f => f.Status == FeedbackStatus.Pending);
            var underReviewCount = feedbacks.Count(f => f.Status == FeedbackStatus.UnderReview);
            var notStartedCount = totalEmployees.Count - (completedCount + pendingCount + underReviewCount);

            var totalEmployeeCountWithRoles = totalEmployees
    .Count(e => e.SystemRoles != null && e.SystemRoles.Any(sr => sr.SystemRoleId == 2));


            var cycleDetailsDto = new CycleDetailsDTO
            {
                TotalEmployeeCount = totalEmployeeCountWithRoles,
                CompletedEmployeeCount = completedCount,
                PendingEmployeeCount = pendingCount,
                UnderReviewEmployeeCount = underReviewCount,
                NotStartedEmployeeCount = notStartedCount
            };


            return Result<CycleDetailsDTO>.Success(cycleDetailsDto);
        }
        catch (Exception ex)
        {
            return Result<CycleDetailsDTO>.Error($"An error occurred while retrieving cycle details: {ex.Message}");
        }
    }
}
