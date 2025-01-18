using Ardalis.Result;
using Ardalis.SharedKernel;
using Bytestrone.AppraisalSystem.Core.Entities.PerformanceIndicatorAggregate;
using Bytestrone.AppraisalSystem.Core.Entities.QuestionAggregate;

namespace Bytestrone.AppraisalSystem.UseCases.Questions.Create;

public class CreateQuestionHandler(
    IRepository<Question> questionRepository,
    IRepository<PerformanceIndicator> indicatorRepository)
    : ICommandHandler<CreateQuestionCommand, Result<int>>
{
    private readonly IRepository<Question> _questionRepository = questionRepository
        ?? throw new ArgumentNullException(nameof(questionRepository));
    private readonly IRepository<PerformanceIndicator> _indicatorRepository = indicatorRepository
        ?? throw new ArgumentNullException(nameof(indicatorRepository));

    public async Task<Result<int>> Handle(CreateQuestionCommand request, CancellationToken cancellationToken)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(request.QuestionText))
                return Result<int>.Error("Question text cannot be empty or null.");

            if (request.IndicatorId <= 0)
                return Result<int>.Error("A valid PerformanceIndicatorId is required.");

            var indicator = await _indicatorRepository.GetByIdAsync(request.IndicatorId, cancellationToken);
            if (indicator == null)
                return Result<int>.NotFound($"Performance indicator with ID {request.IndicatorId} not found.");

            var question = new Question(request.QuestionText, indicator);
            await _questionRepository.AddAsync(question, cancellationToken);

            return Result<int>.Success(question.Id);
        }
        catch (Exception ex)
        {
            return Result<int>.Error($"An error occurred while creating the question: {ex.Message}");
        }
    }
}
