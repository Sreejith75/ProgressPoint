using Bytestrone.AppraisalSystem.UseCases.AppraisalSummaries.AppraiseeAnalysis;
using Bytestrone.AppraisalSystem.UseCases.AppraisalSummaries.ExportSummery;

namespace Bytestrone.AppraisalSystem.UseCases.Interface;
public interface ICsvExportService
{
    byte[] GenerateCsv(IEnumerable<AppraiseeAnalysisExportDTO> data);
}