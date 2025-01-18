using Ardalis.Result;
using Ardalis.SharedKernel;

namespace Bytestrone.AppraisalSystem.UseCases.AppraisalForms.GetFormQuestions;
public record GetFormQuestionsQuery(int EmployeeRoleId,int EmployeeId) : IQuery<Result<FormQuestionGroupedDTO>>;