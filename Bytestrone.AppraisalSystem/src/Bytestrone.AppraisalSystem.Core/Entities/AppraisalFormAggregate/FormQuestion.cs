using Ardalis.GuardClauses;
using Ardalis.SharedKernel;
using Bytestrone.AppraisalSystem.Core.Entities.QuestionAggregate;

namespace Bytestrone.AppraisalSystem.Core.Entities.AppraisalFormAggregate;
public class FormQuestion
{
    public int AppraisalFormId { get; private set; }
    public AppraisalForm? AppraisalForm { get; private set; }
    public int QuestionId { get; private set; }
    public Question Question{ get; private set; }=default!;

    public FormQuestion(int questionId, int appraisalFormId)
    {
        Guard.Against.NegativeOrZero(questionId, nameof(questionId));
        Guard.Against.NegativeOrZero(appraisalFormId, nameof(appraisalFormId));

        QuestionId = questionId;
        AppraisalFormId = appraisalFormId;
    }
}