using Ardalis.Result;
using Ardalis.SharedKernel;

namespace Bytestrone.AppraisalSystem.UseCases.AppraisalSummaries.GetBucketAnalysis;
public record GetbucketAnalysisQuery(int quarterId,int yearId): IQuery<Result<BucketAnalysisResult>>;