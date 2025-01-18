using Ardalis.Specification;

namespace Bytestrone.AppraisalSystem.Core.Entities.AppraisalCycleAggregate.Specification;
public class ActiveCycleSpec : Specification<AppraisalCycle>
{
    public ActiveCycleSpec()
    {
        Query.Where(ac => ac.Status == CycleStatus.InProgress);
    }
}