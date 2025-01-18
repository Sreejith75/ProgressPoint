using Ardalis.GuardClauses;
using Ardalis.Result;
using Ardalis.SharedKernel;
using Bytestrone.AppraisalSystem.Core.Entities.AppraisalCycleAggregate;

namespace Bytestrone.AppraisalSystem.UseCases.AppraisalCycles.Create;
public class CreateAppraisalCycleCommandHandler(IRepository<AppraisalCycle> appraisalCycleRepository) : ICommandHandler<CreateAppraisalCycleCommand, Result<int>>
{
    private readonly IRepository<AppraisalCycle> _appraisalCycleRepository = appraisalCycleRepository;

    public async Task<Result<int>> Handle(CreateAppraisalCycleCommand request, CancellationToken cancellationToken)
    {
        Guard.Against.Null(request.Year, nameof(request.Year));
        Guard.Against.OutOfRange(request.Year, nameof(request.Year), 2024, 2200);
        Guard.Against.Null(request.AppraiseeStartDate, nameof(request.AppraiseeStartDate));
        Guard.Against.Null(request.AppraiseeEndDate, nameof(request.AppraiseeEndDate));
        Guard.Against.Null(request.AppraiserStartDate, nameof(request.AppraiserStartDate));
        Guard.Against.Null(request.AppraiserEndDate, nameof(request.AppraiserEndDate));
        
        
        var appraisalCycle = new AppraisalCycle(
            (Quarter?)request.Quarter,
            request.Year,
            new DateRange(request.AppraiseeStartDate, request.AppraiseeEndDate),
            new DateRange(request.AppraiserStartDate, request.AppraiserEndDate)
        );

        await _appraisalCycleRepository.AddAsync(appraisalCycle, cancellationToken);

        return Result<int>.Success(appraisalCycle.Id);
    }
}
