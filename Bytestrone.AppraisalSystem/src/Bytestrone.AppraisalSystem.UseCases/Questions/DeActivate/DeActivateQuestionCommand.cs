using System.Windows.Input;
using Ardalis.Result;

namespace Bytestrone.AppraisalSystem.UseCases.Questions.DeActivate;
public record DeActivateQuestionCommand(int Id) : Ardalis.SharedKernel.ICommand<Result<int>>;