using System.Windows.Input;
using Ardalis.Result;
using Bytestrone.AppraisalSystem.Core.Entities.QuestionAggregate;

namespace Bytestrone.AppraisalSystem.UseCases.AppraisalForms.Create;
public record CreateAppraisalFormCommand(int EmployeeRoleId, List<int> QuestionIds) :Ardalis.SharedKernel.ICommand<Result<int>>;