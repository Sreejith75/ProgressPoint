using System.Runtime.CompilerServices;
using Ardalis.GuardClauses;
using Ardalis.SharedKernel;
using Bytestrone.AppraisalSystem.Core.Entities.AppraisalCycleAggregate;
using Bytestrone.AppraisalSystem.Core.Entities.EmployeeRoleAggregate;
using Bytestrone.AppraisalSystem.Core.Entities.QuestionAggregate;

namespace Bytestrone.AppraisalSystem.Core.Entities.AppraisalFormAggregate;
public class AppraisalForm : EntityBase, IAggregateRoot
{
    // public int AppraisalCycleId { get; private set; }
    public int EmployeeRoleId { get; private set; }
    public FormStatus Status { get; private set; } = FormStatus.Draft;
    public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;

    public ICollection<FormQuestion> FormQuestionMappings { get; private set; } = new List<FormQuestion>(); 

    // public AppraisalCycle? AppraisalCycle { get; private set; }
    public EmployeeRole? EmployeeRole { get; private set; }

    public AppraisalForm() { }

    public AppraisalForm( int employeeRoleId)
    {
        // Guard.Against.NegativeOrZero(appraisalCycleId, nameof(appraisalCycleId));
        Guard.Against.NegativeOrZero(employeeRoleId, nameof(employeeRoleId));

        // AppraisalCycleId = appraisalCycleId;
        EmployeeRoleId = employeeRoleId;
    }

    public void ActivateForm()
    {
        if (Status != FormStatus.Draft)
            throw new InvalidOperationException("Form must be in Draft status to activate.");

        Status = FormStatus.Active;
    }
    public void DraftForm()
    {
        if (Status != FormStatus.Archived)
            throw new InvalidOperationException("Form must be in Archived status to Draft.");

        Status = FormStatus.Draft;
    }

    public void ArchiveForm()
    {
        if (Status == FormStatus.Archived)
            throw new InvalidOperationException("Form is already archived.");

        Status = FormStatus.Archived;
    }

    public void AddQuestion(Question question)
    {
        if (FormQuestionMappings.Any(fq => fq.QuestionId == question.Id))
            throw new InvalidOperationException("This question is already added to the form.");

        var mapping = new FormQuestion(question.Id, Id);
        FormQuestionMappings.Add(mapping);
    }
    public IEnumerable<Question> GetQuestions()
    {
        return FormQuestionMappings.Select(fq => fq.Question);
    }
}
