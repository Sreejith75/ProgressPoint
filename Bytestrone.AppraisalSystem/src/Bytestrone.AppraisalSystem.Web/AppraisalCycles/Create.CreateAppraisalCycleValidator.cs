using FluentValidation;
using System;

namespace Bytestrone.AppraisalSystem.web.AppraisalCycles;
public class CreateAppraisalCycleValidator : AbstractValidator<CreateAppraisalCycleRequest>
{
    public CreateAppraisalCycleValidator()
    {
        RuleFor(x => x.Quarter)
            .NotEmpty().WithMessage("Quarter is required.")
            .IsInEnum().WithMessage("Quarter must be a valid quarter (e.g., Q1, Q2, Q3, Q4).");

        RuleFor(x => x.Year)
            .GreaterThan(0).WithMessage("Year must be greater than 0.");

        RuleFor(x => x.AppraiseeStartDate)
            .LessThan(x => x.AppraiseeEndDate).WithMessage("Appraisee start date must be before the end date.");

        RuleFor(x => x.AppraiserStartDate)
            .LessThan(x => x.AppraiserEndDate).WithMessage("Appraiser start date must be before the end date.");

        RuleFor(x => x.AppraiseeEndDate)
            .GreaterThan(x => x.AppraiseeStartDate).WithMessage("Appraisee end date must be after the start date.");

        RuleFor(x => x.AppraiserEndDate)
            .GreaterThan(x => x.AppraiserStartDate).WithMessage("Appraiser end date must be after the start date.");
    }
}
