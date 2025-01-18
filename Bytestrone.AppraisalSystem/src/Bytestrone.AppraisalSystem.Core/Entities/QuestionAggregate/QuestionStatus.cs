using Ardalis.SmartEnum;

namespace Bytestrone.AppraisalSystem.Core.ContributorAggregate;

public class QuestionStatus : SmartEnum<QuestionStatus>
{
  public static readonly QuestionStatus Active = new(nameof(Active), 1);
  public static readonly QuestionStatus InActive = new(nameof(InActive), 2);

  protected QuestionStatus(string name, int value) : base(name, value) { }
}

