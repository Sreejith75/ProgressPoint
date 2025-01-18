using Ardalis.SharedKernel;
using Bytestrone.AppraisalSystem.Core.ContributorAggregate;
using Bytestrone.AppraisalSystem.Core.Entities.PerformanceIndicatorAggregate;
namespace Bytestrone.AppraisalSystem.Core.Entities.QuestionAggregate;
public class Question : EntityBase, IAggregateRoot
{
    public string QuestionText { get; private set; }
    public QuestionStatus Status { get; private set; }
    public int PerformanceIndicatorId { get; private set; }
    public PerformanceIndicator Indicator { get; private set; } = default!;

    private Question()
    {
        QuestionText = string.Empty;
        Status = QuestionStatus.Active;
    }

    public Question(string questionText, PerformanceIndicator indicator, QuestionStatus? status = null)
    {
        if (string.IsNullOrWhiteSpace(questionText))
            throw new ArgumentException("Question text cannot be empty or null.", nameof(questionText));

        if (indicator == null)
            throw new ArgumentNullException(nameof(indicator), "Indicator cannot be null.");

        QuestionText = questionText;
        PerformanceIndicatorId = indicator.Id;
        Status = status ?? QuestionStatus.Active;
    }

    public void Activate()
    {
        if (Status == QuestionStatus.Active)
            throw new InvalidOperationException("The question is already active.");

        Status = QuestionStatus.Active;
    }

    public void Deactivate()
    {
        if (Status == QuestionStatus.InActive)
            throw new InvalidOperationException("The question is already inactive.");

        Status = QuestionStatus.InActive;
    }

    public void UpdateText(string newText)
    {
        if (string.IsNullOrWhiteSpace(newText))
            throw new ArgumentException("Question text cannot be empty or null.", nameof(newText));

        QuestionText = newText;
    }

    public bool IsActive() => Status == QuestionStatus.Active;
}
