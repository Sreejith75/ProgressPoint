namespace Bytestrone.AppraisalSystem.Core.Entities.AppraisalFeedbackAggregate;
public partial class AppraisalFeedback
{
    // General method to calculate the appraisal score for either appraisee or appraiser
    private decimal CalculateAppraisalScoreForRole(bool forAppraiser = false)
    {

        if (_feedbackDetails == null || !_feedbackDetails.Any())
            return 0;

        if (Employee?.Role?.DepartmentId == null)
            throw new InvalidOperationException("Employee or department information is missing.");

        // Group feedback details by performance indicator and calculate average scores
        var indicatorScores = _feedbackDetails
            .GroupBy(fd => fd.Question?.PerformanceIndicatorId)
            .Select(group => new
            {
                PerformanceIndicatorId = group.Key,
                AverageScore = forAppraiser
                    ? group.Average(fd => fd.AppraiserRating)
                    : group.Average(fd => fd.AppraiseeRating)
            })
            .Where(score => score.PerformanceIndicatorId.HasValue)
            .ToList();


        decimal totalWeightedScore = 0;
        decimal totalWeight = 0;

        var uniquePerformanceFactors = new HashSet<int>(); 

        // Calculate the weighted score
        foreach (var indicatorScore in indicatorScores)
        {
            var performanceIndicator = _feedbackDetails
                .Select(fd => fd.Question?.Indicator)
                .FirstOrDefault(indicator => indicator?.Id == indicatorScore.PerformanceIndicatorId);

            if (performanceIndicator == null) continue;

            var performanceFactor = performanceIndicator.PerformanceFactor;

            if (performanceFactor == null) continue;

            var rolePerformanceFactor = performanceFactor.DepartmentPerformanceFactors
                .FirstOrDefault(rpf => rpf.DepartmentId == Employee.Role.DepartmentId);

            if (rolePerformanceFactor == null) continue;

            var indicatorWeight = performanceIndicator.Weightage;
            var factorWeight = rolePerformanceFactor.Weightage;

            // Only add factor weight once for each unique factor
            if (!uniquePerformanceFactors.Contains(performanceFactor.Id))
            {
                uniquePerformanceFactors.Add(performanceFactor.Id);
                totalWeight += factorWeight;
            }


            // Calculate the weighted score if valid weights exist
            if (indicatorWeight > 0 && factorWeight > 0)
            {
                totalWeightedScore += (decimal)indicatorScore.AverageScore! * indicatorWeight * factorWeight;
            }
        }

        // Return the calculated score, or 0 if no valid data
        var totalScore = totalWeight > 0 ? totalWeightedScore / totalWeight : 0;
        return totalScore;

    }

    // Method to calculate the appraisee score
    public decimal CalculateAppraiseeScore()
    {
        return CalculateAppraisalScoreForRole(forAppraiser: false);
    }

    // Method to calculate the appraiser score
    public decimal CalculateAppraiserScore()
    {
        return CalculateAppraisalScoreForRole(forAppraiser: true);
    }
}
