using Bytestrone.AppraisalSystem.Web.SystemRole;
using FastEndpoints;
using FluentValidation;

namespace Bytestrone.AppraisalSystem.Web.SystemRole;

/// <summary>
/// See: https://fast-endpoints.com/docs/validation
/// </summary>
public class DeleteSystemRoleValidator : Validator<DeleteSystemRoleRequest>
{
  public DeleteSystemRoleValidator()
  {
    RuleFor(x => x.SystemRoleId)
      .GreaterThan(0);
  }
}
