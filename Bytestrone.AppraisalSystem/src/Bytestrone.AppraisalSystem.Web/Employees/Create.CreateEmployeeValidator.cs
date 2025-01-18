using FluentValidation;

namespace Bytestrone.AppraisalSystem.Web.Employees;
public class CreateEmployeeRequestValidator : AbstractValidator<CreateEmployeeRequest>
{
    public CreateEmployeeRequestValidator()
    {
        RuleFor(x => x.FirstName)
            .NotEmpty().WithMessage("First name is required.");

        RuleFor(x => x.LastName)
            .NotEmpty().WithMessage("Last name is required.");

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required.")
            .EmailAddress().WithMessage("Invalid email format.");

        RuleFor(x => x.PasswordHash)
            .NotEmpty().WithMessage("Password is required.");

        RuleFor(x => x.EmployeeRoleId)
            .GreaterThan(0).WithMessage("Employee Role ID must be greater than 0.");

        RuleFor(x => x.SystemRoleIds)
            .NotNull().WithMessage("System Role IDs are required.")
            .Must(roleIds => roleIds.Count > 0).WithMessage("At least one system role is required.");
    }
}
