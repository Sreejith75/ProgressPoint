using Ardalis.SharedKernel;
using Ardalis.Result;
using Bytestrone.AppraisalSystem.Core.Entities.AppraisalFormAggregate;
using Bytestrone.AppraisalSystem.Core.Entities.Questions.Specification;

using Bytestrone.AppraisalSystem.Core.Entities.QuestionAggregate;

namespace Bytestrone.AppraisalSystem.UseCases.AppraisalForms.Create;
public class CreateAppraisalFormHandler(IRepository<Question> questionRepository, IRepository<AppraisalForm> repository) : ICommandHandler<CreateAppraisalFormCommand, Result<int>>
{
    private readonly IRepository<Question> _questionRepository = questionRepository;
    private readonly IRepository<AppraisalForm> _repository = repository;

  public async Task<Result<int>> Handle(CreateAppraisalFormCommand request, CancellationToken cancellationToken)
    {
        var specification = new GetQuestionsByIdsSpecification(request.QuestionIds);
        var questions = await _questionRepository.ListAsync(specification, cancellationToken);

        if (questions.Count != request.QuestionIds.Count)
        {
            return Result<int>.Error("One or more questions were not found.");
        }
        
        var appraisalForm = new AppraisalForm(request.EmployeeRoleId);

        if (request.QuestionIds == null || !request.QuestionIds.Any())
        {
            return Result<int>.Error("Questions cannot be null or empty.");
        }

        await _repository.AddAsync(appraisalForm, cancellationToken);
        await _repository.SaveChangesAsync(cancellationToken);

        foreach (var question in questions)
        {
            appraisalForm.AddQuestion(question);
        }

        await _repository.SaveChangesAsync(cancellationToken);

        return Result<int>.Success(appraisalForm.Id);
    }
}