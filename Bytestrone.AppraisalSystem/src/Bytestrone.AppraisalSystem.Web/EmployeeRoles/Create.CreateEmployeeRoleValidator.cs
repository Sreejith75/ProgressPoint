using System.ComponentModel.DataAnnotations;
using FastEndpoints;
using FluentValidation;

namespace Bytestrone.AppraisalSystem.Web.EmployeeRoles;
public class CreateEmployeeRoleValidator : Validator<CreateEmployeeRoleRequest>
{
    public CreateEmployeeRoleValidator()
    {
        RuleFor(x => x.RoleName)
        .NotEmpty()
        .WithMessage("Role name is required");
        RuleFor(x => x.EmployeeRoleCode)
        .NotEmpty()
        .WithMessage("Employee role code required");
        RuleFor(x=>x.DepartmentId)
        .NotNull()
        .WithMessage("DepartmentID Cannot be null");
    }
}