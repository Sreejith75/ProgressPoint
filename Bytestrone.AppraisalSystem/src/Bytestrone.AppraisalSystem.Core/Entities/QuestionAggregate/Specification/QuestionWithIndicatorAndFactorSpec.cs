using Ardalis.Specification;
using Bytestrone.AppraisalSystem.Core.ContributorAggregate;
using Bytestrone.AppraisalSystem.Core.Entities.QuestionAggregate;

namespace Bytestrone.AppraisalSystem.Core.Entities.QuestionAggregate.Specification;
public class QuestionsWithIndicatorAndFactorSpec : Specification<Question>
{
    public QuestionsWithIndicatorAndFactorSpec()
    {
        Query
            .Include(q => q.Indicator!)
            .ThenInclude(i => i.PerformanceFactor!)

            .Where(q => q.Status == QuestionStatus.Active)
            .AsNoTracking();
    }
}
