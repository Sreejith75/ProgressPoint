using CsvHelper;
using CsvHelper.Configuration;
using Bytestrone.AppraisalSystem.UseCases.AppraisalSummaries.ExportSummery;
using FastEndpoints;
using MediatR;
using Microsoft.AspNetCore.Http;
using System.Globalization;
using System.IO;
using Bytestrone.AppraisalSystem.web.AppraisalSummaries;

namespace Bytestrone.AppraisalSystem.Web.AppraisalSummaries;
public class Export(IMediator mediator) : Endpoint<ExportSummeryRequest>
{
    private readonly IMediator _mediator = mediator;

  public override void Configure()
    {
        Get("/AppraisalSummary/Analysis/Export");
        AllowAnonymous();
    }

    public override async Task HandleAsync(ExportSummeryRequest req, CancellationToken ct)
    {
        var query = new ExportSummeryQuery(req.Quarter, req.Year, req.DepartmentId, req.RoleId);

        var result = await _mediator.Send(query, ct);

        if (result.IsSuccess)
        {
            var csvBytes = result.Value;

            HttpContext.Response.ContentType = "application/csv"; 

            HttpContext.Response.Headers.Append("Content-Disposition", "attachment; filename=appraisee_analysis.csv");

            await HttpContext.Response.Body.WriteAsync(csvBytes, ct);
        }
        else
        {
            HttpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
            await HttpContext.Response.WriteAsJsonAsync(new { message = string.Join(", ", result.Errors) }, ct);
        }
    }
}