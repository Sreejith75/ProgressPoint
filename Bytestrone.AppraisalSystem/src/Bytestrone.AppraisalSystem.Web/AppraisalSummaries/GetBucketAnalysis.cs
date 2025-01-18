using Bytestrone.AppraisalSystem.UseCases.AppraisalSummaries.GetBucketAnalysis;
using FastEndpoints;
using Google.Apis.Download;
using MediatR;

namespace Bytestrone.AppraisalSystem.web.AppraisalSummaries;
public class GetBucketAnalysis(IMediator mediator): Endpoint<BucketAnalysisRequest,BucketAnalysisResponse>
{
  public override void Configure()
  {
    Get("/AppraisalSummary/BucketAnalysis/{QuarterId:int}/{Year:int}");
    AllowAnonymous();
  }
  public override async Task HandleAsync(BucketAnalysisRequest req, CancellationToken ct)
  {
    var query = new GetbucketAnalysisQuery(req.QuarterId,req.Year);
    var result = await mediator.Send(query, ct);

    if(result.IsSuccess)
    {

      Response = new BucketAnalysisResponse
      {
        bucketAnalysisDetails=result.Value.BucketAnalysis,
        TotalFeedbackCount=result.Value.TotalSummariesCount,
        Message ="Success"
      };
      return;
    }
    else
    {
      Response = new BucketAnalysisResponse
      {
        Message ="Failed",
        TotalFeedbackCount=result.Value.TotalSummariesCount,
        bucketAnalysisDetails=[]
      };
      return;
    }

  }
}