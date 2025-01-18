using Ardalis.Result;
using Ardalis.SharedKernel;
using Bytestrone.AppraisalSystem.Core.EmployeeAggregate;
using Bytestrone.AppraisalSystem.Core.Entities.AppraisalFeedbackAggregate;
using Bytestrone.AppraisalSystem.Core.Entities.AppraisalFeedbackAggregate.Specification;
using Bytestrone.AppraisalSystem.Core.Entities.AppraisalSummeryAggregate;
using Bytestrone.AppraisalSystem.Core.Entities.AppraisalSummeryAggregate.Specification;
using Bytestrone.AppraisalSystem.Core.Entities.EmployeeAggregate.Specification;

namespace Bytestrone.AppraisalSystem.UseCases.AppraisalFeedbacks.UpdateAppraiserFeedback;

public class UpdateAppraiserFeedbackCommandHandler(IRepository<AppraisalFeedback> appraisalFeedbackRepository, IRepository<Employee> employeeRepository,
    IRepository<AppraisalSummary> appraisalSummaryRepository) : ICommandHandler<UpdateAppraiserFeedbackCommand, Result<AppraisalFeedbackResponseDTO>>
{
    private readonly IRepository<AppraisalFeedback> _appraisalFeedbackRepository = appraisalFeedbackRepository;
    private readonly IRepository<AppraisalSummary> _appraisalSummaryRepository = appraisalSummaryRepository;
    private readonly IRepository<Employee> _employeeRepository = employeeRepository;


    public async Task<Result<AppraisalFeedbackResponseDTO>> Handle(UpdateAppraiserFeedbackCommand request, CancellationToken cancellationToken)
    {
        var feedbackSpec = new AppraisalFeedbackByIdSpec(request.FeedbackId);
        var feedback = await _appraisalFeedbackRepository.FirstOrDefaultAsync(feedbackSpec, cancellationToken);

        if (feedback == null)
        {
            return Result<AppraisalFeedbackResponseDTO>.NotFound($"Feedback with ID {request.FeedbackId} was not found.");
        }
        var appraiserSpec = new EmployeeByIdSpec(request.ApprasierId);
        var appraiser = await _employeeRepository.FirstOrDefaultAsync(appraiserSpec, cancellationToken);
        if (appraiser != null)
        {

            feedback.AppraiserId = appraiser.Id;
        }
        foreach (var detail in request.AppraiserFeedbackDetails)
        {
            var feedbackDetail = feedback.FeedbackDetails.FirstOrDefault(q => q.QuestionId == detail.QuestionId);
            if (feedbackDetail != null)
            {
                try
                {
                    feedbackDetail.UpdateAppraiserFeedback(detail.AppraiserRating, detail.AppraiserComment);
                }
                catch (ArgumentOutOfRangeException ex)
                {
                    return Result<AppraisalFeedbackResponseDTO>.Error(ex.Message);
                }
                catch (ArgumentNullException ex)
                {
                    return Result<AppraisalFeedbackResponseDTO>.Error(ex.Message);
                }
            }
            else
            {
                return Result<AppraisalFeedbackResponseDTO>.Error($"Question with ID {detail.QuestionId} was not found in feedback.");
            }
        }
        
        await _appraisalFeedbackRepository.UpdateAsync(feedback, cancellationToken);


        var latestFeedbackSpec = new AppraisalFeedbackByIdSpec(feedback.Id);
        var latestFeedback = await _appraisalFeedbackRepository.FirstOrDefaultAsync(latestFeedbackSpec, cancellationToken);

        if (latestFeedback == null)
        {
            return Result.Error("Failed to retrieve updated appraisal feedback.");
        }

        var totalAppraiserScore = latestFeedback.CalculateAppraiserScore();

        var summarySpecForCreation = new GetSummeryByEmployeeIdCycleIdSpec(latestFeedback.EmployeeId, latestFeedback.CycleId);
        var summaryForCreation = await _appraisalSummaryRepository.FirstOrDefaultAsync(summarySpecForCreation, cancellationToken);

        if (summaryForCreation == null)
        {
            return Result.Error("No appraisal summary found for the given employee and cycle.");
        }

        summaryForCreation.UpdateAppraiserScore(totalAppraiserScore);
        await _appraisalSummaryRepository.UpdateAsync(summaryForCreation, cancellationToken);
        
        var feedbackDto = new AppraisalFeedbackResponseDTO
        {
            Id = feedback.Id,
            FinalScore = summaryForCreation.AppraiserScore,
            PerformanceBucket = summaryForCreation.PerformanceBucket.Name
        };

        return Result<AppraisalFeedbackResponseDTO>.Success(feedbackDto);
    }
}
