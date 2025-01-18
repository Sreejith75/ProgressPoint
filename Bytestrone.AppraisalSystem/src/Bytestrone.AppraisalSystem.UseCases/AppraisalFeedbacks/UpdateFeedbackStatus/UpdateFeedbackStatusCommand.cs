using System.Windows.Input;
using Ardalis.Result;
using Ardalis.SharedKernel;

namespace Bytestrone.AppraisalSystem.UseCases.AppraisalFeedbacks.UpdateFeedbackStatus;
public record UpdateFeedbackStatusCommand(int FeedbackId): ICommand<Result<int>>;