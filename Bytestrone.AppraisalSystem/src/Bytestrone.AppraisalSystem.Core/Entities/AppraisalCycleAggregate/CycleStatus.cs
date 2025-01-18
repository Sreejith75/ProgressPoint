using Ardalis.SmartEnum;

namespace Bytestrone.AppraisalSystem.Core.Entities.AppraisalCycleAggregate;

public class CycleStatus : SmartEnum<CycleStatus>
{
    public static readonly CycleStatus NotStarted = new(nameof(NotStarted), 1);
    public static readonly CycleStatus InProgress = new(nameof(InProgress), 2);
    public static readonly CycleStatus Completed = new(nameof(Completed), 3);

    protected CycleStatus(string name, int value) : base(name, value) { }

    
}
