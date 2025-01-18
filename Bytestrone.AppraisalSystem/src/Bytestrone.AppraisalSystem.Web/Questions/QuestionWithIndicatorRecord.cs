

using Bytestrone.AppraisalSystem.web.PerformanceIndicators;

namespace Bytestrone.AppraisalSystem.web.Questions;
public record QuestionWithIndicatorRecord(
    int QuestionId,
    string QuestionText,
    PerformanceIndicatorRecord Indicator
);
