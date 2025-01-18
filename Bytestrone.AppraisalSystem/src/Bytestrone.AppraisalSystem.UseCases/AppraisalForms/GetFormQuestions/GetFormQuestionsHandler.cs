using Ardalis.Result;
using Bytestrone.AppraisalSystem.Core.Entities.AppraisalFormAggregate;
using Bytestrone.AppraisalSystem.Core.Entities.PerformanceIndicatorAggregate;
using Ardalis.GuardClauses;
using Ardalis.SharedKernel;
using Bytestrone.AppraisalSystem.Core.Entities.AppraisalFormAggregate.Specification;
using Bytestrone.AppraisalSystem.Core.EmployeeAggregate;
using Bytestrone.AppraisalSystem.Core.Entities.EmployeeAggregate.Specification;
using Bytestrone.AppraisalSystem.UseCases.Questions;
using Bytestrone.AppraisalSystem.UseCases.PerformanceIndicators;
using Bytestrone.AppraisalSystem.UseCases.PerformanceFactors;
using Bytestrone.AppraisalSystem.Core.Entities.PerformanceIndicatorAggregate.Specification;

namespace Bytestrone.AppraisalSystem.UseCases.AppraisalForms.GetFormQuestions;

public class GetFormQuestionsHandler(
    IRepository<AppraisalForm> appraisalFormRepository,
    IRepository<PerformanceIndicator> performanceIndicatorRepository
) : IQueryHandler<GetFormQuestionsQuery, Result<FormQuestionGroupedDTO>>
{
    private readonly IRepository<AppraisalForm> _appraisalFormRepository = appraisalFormRepository;
    private readonly IRepository<PerformanceIndicator> _performanceIndicatorRepository = performanceIndicatorRepository;

    public async Task<Result<FormQuestionGroupedDTO>> Handle(GetFormQuestionsQuery request, CancellationToken cancellationToken)
    {
        Guard.Against.NegativeOrZero(request.EmployeeRoleId, nameof(request.EmployeeRoleId));

        // Fetch the form
        var form = await _appraisalFormRepository.FirstOrDefaultAsync(
            new GetFormQuestionsSpec(request.EmployeeRoleId, FormStatus.Active), cancellationToken);

        if (form == null)
        {
            return Result<FormQuestionGroupedDTO>.NotFound($"Form not found for RoleId: {request.EmployeeRoleId}");
        }

        // Get questions and their related performance indicators
        var questions = form.GetQuestions();
        if (questions == null || !questions.Any())
        {
            return Result<FormQuestionGroupedDTO>.NotFound("No questions associated with the form.");
        }

        var indicatorIds = questions.Select(q => q.PerformanceIndicatorId).Distinct();
        var indicators = await _performanceIndicatorRepository.ListAsync(
            new GetIndicatorsByIdsSpec(indicatorIds), cancellationToken);

        // Filter questions for each indicator under their respective factor
        var groupedIndicators = questions
            .GroupBy(q => q.PerformanceIndicatorId)
            .Select(g =>
            {
                var indicator = indicators.FirstOrDefault(i => i.Id == g.Key);
                if (indicator == null) return null;

                // Filter questions specific to this indicator
                var filteredQuestions = g.Where(q => /* Your filtering logic */ true)
                                          .Select(q => new QuestionsDTO
                                          {
                                              QuestionId = q.Id,
                                              QuestionText = q.QuestionText
                                          }).ToList();

                return new
                {
                    Indicator = indicator,
                    Questions = filteredQuestions
                };
            })
            .Where(x => x != null && x.Questions.Any())
            .GroupBy(x => x!.Indicator!.PerformanceFactor!.Id) // Group by factor ID
            .ToList();

        // Map grouped data into hierarchical DTOs
        var factorDtos = groupedIndicators.Select(factorGroup =>
        {
            var factor = factorGroup.First()?.Indicator?.PerformanceFactor;
            return new PerformanceFactorsDTO
            {
                FactorId = factor?.Id ?? 0,
                FactorName = factor?.FactorName ?? "No Factor",
                Indicators = factorGroup.Select(indicatorGroup => new PerformanceIndicatorDTO
                {
                    IndicatorId = indicatorGroup!.Indicator!.Id,
                    IndicatorName = indicatorGroup.Indicator.IndicatorName,
                    Weightage = indicatorGroup.Indicator.Weightage,
                    Questions = indicatorGroup.Questions 
                }).ToList()
            };
        }).ToList();

        // Return the structured result
        var result = new FormQuestionGroupedDTO
        {
            FormId = form.Id,
            Factors = factorDtos
        };

        return Result<FormQuestionGroupedDTO>.Success(result);
    }

}
