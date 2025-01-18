using FluentValidation;

namespace Bytestrone.AppraisalSystem.web.AppraisalForms;
    public class CreateAppraisalFormValidator : AbstractValidator<CreateAppraisalFormRequest>
    {
        public CreateAppraisalFormValidator()
        {
            
            
            RuleFor(x => x.EmployeeRoleId)
                .GreaterThan(0).WithMessage("Role ID must be a positive integer.");
            
            RuleFor(x => x.QuestionIds)
                .NotNull().WithMessage("Question IDs cannot be null.")
                .NotEmpty().WithMessage("Question IDs cannot be empty.");
        }
    }
