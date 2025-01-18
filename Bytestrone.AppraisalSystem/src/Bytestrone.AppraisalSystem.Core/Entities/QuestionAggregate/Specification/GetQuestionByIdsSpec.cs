using Ardalis.Specification;
using Bytestrone.AppraisalSystem.Core.Entities;
using Bytestrone.AppraisalSystem.Core.Entities.QuestionAggregate;
using System.Collections.Generic;

namespace Bytestrone.AppraisalSystem.Core.Entities.Questions.Specification;
    public class GetQuestionsByIdsSpecification : Specification<Question>
    {
        public GetQuestionsByIdsSpecification(List<int> questionIds)
        {
            Query.Where(q => questionIds.Contains(q.Id));
        }
    }
