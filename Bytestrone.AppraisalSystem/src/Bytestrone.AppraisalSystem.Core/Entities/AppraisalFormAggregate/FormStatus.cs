using Ardalis.SmartEnum;

namespace Bytestrone.AppraisalSystem.Core.Entities.AppraisalFormAggregate;
public class FormStatus : SmartEnum<FormStatus>
{
    public static readonly FormStatus Draft = new FormStatus(nameof(Draft), 1);
    public static readonly FormStatus Active = new FormStatus(nameof(Active), 2);
    public static readonly FormStatus Archived = new FormStatus(nameof(Archived), 3);

    protected FormStatus(string name, int value) : base(name, value) { }

}
