using Ardalis.Result;
using Ardalis.SharedKernel;
using Bytestrone.AppraisalSystem.Core.Entities.QuestionAggregate;
using Bytestrone.AppraisalSystem.Core.Entities.QuestionAggregate.Specification;

namespace Bytestrone.AppraisalSystem.UseCases.Questions.DeActivate;

public class DeactivateQuestionHandler : ICommandHandler<DeActivateQuestionCommand, Result<int>>
{
    private readonly IRepository<Question> _repository;

    public DeactivateQuestionHandler(IRepository<Question> repository)
    {
        _repository = repository;
    }

    public async Task<Result<int>> Handle(DeActivateQuestionCommand request, CancellationToken cancellationToken)
    {
        var question = await _repository.FirstOrDefaultAsync(new GetQuestionByIdSpec(request.Id), cancellationToken);

        if (question == null)
        {
            return Result<int>.NotFound("Question not found.");
        }

        question.Deactivate();

        await _repository.SaveChangesAsync(cancellationToken);

        return Result.Success(request.Id);
    }
}
