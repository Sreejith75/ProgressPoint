

namespace Bytestrone.AppraisalSystem.UseCases.AppraisalFeedbacks.GetAppraiseeFeedbacks;
public class AppraiseeFeedbackDetailDTO
{
    public int feedbackId { get; set; }
    public string? feedbackStatus { get; set;} 
    public string? AppraiseeName { get; set; }
    public decimal AppraiseeScore { get; set; }
    public string? PerformanceBucket { get; set; }
    public List<PerformanceFactorsDTO>? factors { get; set; }
}

public class PerformanceFactorsDTO
{
    public int FactorId { get; set; }
    public string? FactorName { get; set; }
    public List<PerformanceIndicatorDTO>? Indicators { get; set; }
}

public class PerformanceIndicatorDTO
{
    public int IndicatorId { get; set; }
    public string? IndicatorName { get; set; }
    public List<QuestionsDTO>? Question {  get; set; }
}


public class QuestionsDTO
{
    public int QuestId { get; set;}
    public string? QuestionText { get; set; }
    public int AppraiseeRating { get; set; }
    public string? AppraiseeComment { get; set; }
    public int? AppraiserRating { get; set; }
    public string? AppraiserComment {get; set;}
}