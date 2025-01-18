using Bytestrone.AppraisalSystem.Core.Entities.QuestionAggregate;
using Bytestrone.AppraisalSystem.UseCases.Questions;
using Bytestrone.AppraisalSystem.web.Questions;
using Bytestrone.AppraisalSystem.Web.Questions;

namespace Bytestrone.AppraisalSystem.web.AppraisalForms;
public record FormQuestionRecord(
    int FormId,
    int EmployeeRoleId,
    List<QuestionRecord> Questions
);