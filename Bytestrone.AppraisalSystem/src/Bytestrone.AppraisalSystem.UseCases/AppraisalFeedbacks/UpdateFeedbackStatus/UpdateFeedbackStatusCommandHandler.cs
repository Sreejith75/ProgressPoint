using System.Security.Cryptography.X509Certificates;
using Ardalis.Result;
using Ardalis.SharedKernel;
using Bytestrone.AppraisalSystem.Core.Entities.AppraisalFeedbackAggregate;
using Bytestrone.AppraisalSystem.Core.Entities.AppraisalFeedbackAggregate.Specification;

namespace Bytestrone.AppraisalSystem.UseCases.AppraisalFeedbacks.UpdateFeedbackStatus;

public class UpdateFeedbackStatusCommandHandler : ICommandHandler<UpdateFeedbackStatusCommand, Result<int>>
{
    private readonly IRepository<AppraisalFeedback> _feedbackRepository;

    public UpdateFeedbackStatusCommandHandler(IRepository<AppraisalFeedback> feedbackRepository)
    {
        _feedbackRepository = feedbackRepository;
    }

    public async Task<Result<int>> Handle(UpdateFeedbackStatusCommand request, CancellationToken cancellationToken)
    {
        var feedbackSpec = new AppraisalFeedbackByIdSpec(request.FeedbackId);
        var feedback = await _feedbackRepository.FirstOrDefaultAsync(feedbackSpec, cancellationToken);

        if (feedback == null)
        {
            return Result<int>.NotFound($"Feedback with ID {request.FeedbackId} not found.");
        }

        foreach (var detail in feedback.FeedbackDetails)
        {
            if (detail.Status==FeedbackStatus.Pending)
            {
                detail.Status = FeedbackStatus.UnderReview;
            }
            else if (detail.Status == FeedbackStatus.UnderReview)
            {
                detail.Status = FeedbackStatus.Completed;
            }
        }

        if (feedback.Status==FeedbackStatus.Pending)
        {
            feedback.Status= FeedbackStatus.UnderReview;
        }
        else if(feedback.Status==FeedbackStatus.UnderReview)
        {
            feedback.Status=FeedbackStatus.Completed;
        }
        

        await _feedbackRepository.UpdateAsync(feedback, cancellationToken);

        return Result<int>.Success(feedback.Id);
    }
}
