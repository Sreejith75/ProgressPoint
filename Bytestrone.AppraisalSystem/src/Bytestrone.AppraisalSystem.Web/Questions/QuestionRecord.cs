using Bytestrone.AppraisalSystem.UseCases.PerformanceIndicators;

namespace Bytestrone.AppraisalSystem.web.Questions;
public record QuestionRecord(
    int QuestionId,
    string QuestionText,
    PerformanceIndicatorWithFactorDTO Indicator
);