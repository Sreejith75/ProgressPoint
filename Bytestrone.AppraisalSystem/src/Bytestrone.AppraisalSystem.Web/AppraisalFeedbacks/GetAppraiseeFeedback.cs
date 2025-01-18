using Bytestrone.AppraisalSystem.UseCases.AppraisalFeedbacks.GetAppraiseeFeedbacks;
using Bytestrone.AppraisalSystem.web.AppraisalFeedbacks;
using FastEndpoints;
using MediatR;

namespace Bytestrone.AppraisalSystem.Web.AppraisalFeedbacks;

public class GetAppraisalFeedbackEndpoint(IMediator mediator) : Endpoint<AppraiseeFeedbackRequest, AppraiseeFeedbackResponse>
{
    private readonly IMediator _mediator = mediator;

    public override void Configure()
    {
        Get("/appraisal-feedback/{EmployeeId:int}/{CycleId:int}");
        AllowAnonymous();
    }

    public override async Task HandleAsync(AppraiseeFeedbackRequest req, CancellationToken ct)
    {
        var query = new GetAppraiseeFeedbacksQuery(req.EmployeeId, req.CycleId);
        var result = await _mediator.Send(query, ct);

        if (result.IsSuccess)
        {
            var dto = result.Value;

            // Map DTO to response model
            Response = new AppraiseeFeedbackResponse
            {
                Status = true,
                FeedbackId = dto.feedbackId,
                FeedbackStatus = dto.feedbackStatus,
                AppraiseeName =dto.AppraiseeName,
                AppraiseeScore=dto.AppraiseeScore,
                PerformanceBucket=dto.PerformanceBucket,
                Factors = dto.factors?.Select(factor => new PerformanceFactorsDTO
                {
                    FactorId = factor.FactorId,
                    FactorName = factor.FactorName,
                    Indicators = factor.Indicators?.Select(indicator => new PerformanceIndicatorDTO
                    {
                        IndicatorId = indicator.IndicatorId,
                        IndicatorName = indicator.IndicatorName,
                        Question = indicator.Question?.Select(question => new QuestionsDTO
                        {
                            QuestId = question.QuestId,
                            QuestionText = question.QuestionText,
                            AppraiseeRating = question.AppraiseeRating,
                            AppraiseeComment = question.AppraiseeComment,
                            AppraiserRating=question.AppraiserRating,
                            AppraiserComment=question.AppraiserComment
                            
                        }).ToList()
                    }).ToList()
                }).ToList()
            };
        }
        else
        {
            // Handle failure case
            Response = new AppraiseeFeedbackResponse
            {
                Status = false,
                ErrorMessage = result.Errors.FirstOrDefault()
            };
        }
    }
}
