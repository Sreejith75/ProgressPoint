using Ardalis.SmartEnum;
namespace Bytestrone.AppraisalSystem.Core.Entities.AppraisalFeedbackAggregate;
public class FeedbackStatus : SmartEnum<FeedbackStatus>
{
    public static readonly FeedbackStatus NotSet = new(nameof(NotSet), 0);
    public static readonly FeedbackStatus Pending = new(nameof(Pending), 1);
    public static readonly FeedbackStatus UnderReview = new(nameof(UnderReview), 2);
    public static readonly FeedbackStatus Completed = new(nameof(Completed), 3);

    protected FeedbackStatus(string name, int value) : base(name, value) { }
}
