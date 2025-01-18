using Bytestrone.AppraisalSystem.Infrastructure.Data.Config;
using FastEndpoints;
using FluentValidation;

namespace Bytestrone.AppraisalSystem.Web.Questions;
public class CreateQuestionValidator : Validator<CreateQuestionRequest>
{
    public CreateQuestionValidator()
    {
        RuleFor(x => x.QuestionText)
          .NotEmpty()
          .WithMessage("QuestionText is required.");
        RuleFor(x => x.IndicatorId)
        .NotNull()
        .WithMessage("Indicator ID required");
    }
}
