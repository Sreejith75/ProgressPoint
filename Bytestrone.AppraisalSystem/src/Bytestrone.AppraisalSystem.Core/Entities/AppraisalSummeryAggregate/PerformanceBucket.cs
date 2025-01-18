using Ardalis.GuardClauses;
using Ardalis.SmartEnum;

namespace Bytestrone.AppraisalSystem.Core.Entities.AppraisalSummeryAggregate;
public class PerformanceBucket : SmartEnum<PerformanceBucket>
{
    public static readonly PerformanceBucket Exceptional = new(nameof(Exceptional), 5, 4.5m, decimal.MaxValue, "Performance is above and beyond all expectations and in all areas");
    public static readonly PerformanceBucket Outstanding = new(nameof(Outstanding), 4, 4.0m, 4.49m, "Outstanding performance that exceeds most of the expectations");
    public static readonly PerformanceBucket Good = new(nameof(Good), 3, 3.5m, 3.99m, "Consistent performance, and meets or exceeds most of the expectations");
    public static readonly PerformanceBucket Fair = new(nameof(Fair), 2, 3.0m, 3.49m, "Reasonable performance that meets most of the expectations");
    public static readonly PerformanceBucket NeedsImprovement = new(nameof(NeedsImprovement), 1, decimal.MinValue, 2.99m, "Below average performance and does not meet some of the expectations");
    public static readonly PerformanceBucket NotSet = new(nameof(NotSet), 0, decimal.MinValue, 0, "Not started the apprisal process yet");
    
    public decimal MinScore { get; }
    public decimal MaxScore { get; }
    public string Description { get; }

    protected PerformanceBucket(string name, int value, decimal minScore, decimal maxScore, string description)
        : base(name, value)
    {
        if (minScore > maxScore)
            throw new ArgumentException("MinScore cannot be greater than MaxScore.");

        MinScore = minScore;
        MaxScore = maxScore;
        Description = description;
    }

    public static PerformanceBucket GetBucketForScore(decimal? score)
    {
        Guard.Against.Null(score);
        return List.FirstOrDefault(bucket => bucket.IsScoreInRange(score)) ??
               throw new InvalidOperationException($"No performance bucket found for score: {score}");
    }

    public bool IsScoreInRange(decimal? score) => score >= MinScore && score <= MaxScore;
}