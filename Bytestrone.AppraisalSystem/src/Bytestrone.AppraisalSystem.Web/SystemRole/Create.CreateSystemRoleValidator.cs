using System.ComponentModel.DataAnnotations;
using System.Data;
using FastEndpoints;
using FluentValidation;

namespace Bytestrone.AppraisalSystem.Web.SystemRole;
public class CreateSystemRoleValidator : Validator<CreateSystemRoleRequest>
{
    public CreateSystemRoleValidator()
    {
        RuleFor(s=>s.SystemRoleName)
        .NotEmpty()
        .WithMessage("System role name required");
        RuleFor(s=>s.Description)
        .NotEmpty()
        .WithMessage("Description required");
    }
}