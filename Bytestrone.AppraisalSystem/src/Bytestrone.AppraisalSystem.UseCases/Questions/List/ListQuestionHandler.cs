using System.Linq;
using Ardalis.Result;
using Ardalis.SharedKernel;
using Ardalis.Specification;
using Bytestrone.AppraisalSystem.Core.Entities.QuestionAggregate;
using Bytestrone.AppraisalSystem.Core.Entities.QuestionAggregate.Specification;
using Bytestrone.AppraisalSystem.UseCases.PerformanceFactors;
using Bytestrone.AppraisalSystem.UseCases.PerformanceIndicators;

namespace Bytestrone.AppraisalSystem.UseCases.Questions.List;

public class ListQuestionHandler : IQueryHandler<ListQuestionQuery, Result<IEnumerable<QuestionDTO>>>
{
    private readonly IRepository<Question> _questionRepository;

    public ListQuestionHandler(IRepository<Question> questionRepository)
    {
        _questionRepository = questionRepository ?? throw new ArgumentNullException(nameof(questionRepository));
    }

    public async Task<Result<IEnumerable<QuestionDTO>>> Handle(ListQuestionQuery request, CancellationToken cancellationToken)
    {
        // Fetch questions using specification
        var spec = new QuestionsWithIndicatorAndFactorSpec();
        var questions = await _questionRepository.ListAsync(spec, cancellationToken);

        // Check if questions exist
        if (questions == null || !questions.Any())
        {
            return Result.NotFound("No questions found.");
        }

        // Map to DTOs
        var questionDTOs = questions.Select(q => new QuestionDTO
        {
            QuestionId = q.Id,
            QuestionText = q.QuestionText,
            Indicator = q.Indicator == null 
                ? null 
                : new PerformanceIndicatorWithFactorDTO(
                    q.Indicator.Id,
                    q.Indicator.IndicatorName,
                    q.Indicator.Weightage,
                    q.Indicator.PerformanceFactor == null 
                        ? null 
                        : new PerformanceFactorDTO(
                            q.Indicator.PerformanceFactor.Id,
                            q.Indicator.PerformanceFactor.FactorName
                        ))
        });

        return Result.Success(questionDTOs);
    }
}
