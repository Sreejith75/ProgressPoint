
using Ardalis.SmartEnum;

namespace Bytestrone.AppraisalSystem.Core.Entities.AppraisalCycleAggregate;
public class Quarter : SmartEnum<Quarter>
{
    public static readonly Quarter Q1 = new(nameof(Q1), 1);
    public static readonly Quarter Q2 = new(nameof(Q2), 2);
    public static readonly Quarter Q3 = new(nameof(Q3), 3);
    public static readonly Quarter Q4 = new(nameof(Q4), 4);

    protected Quarter(string quarter, int value) : base(quarter,value) { }
    
}