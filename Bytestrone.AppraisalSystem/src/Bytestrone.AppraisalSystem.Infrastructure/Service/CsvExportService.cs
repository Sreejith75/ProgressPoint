using Bytestrone.AppraisalSystem.UseCases.AppraisalSummaries.ExportSummery;
using Bytestrone.AppraisalSystem.UseCases.Interface;
using CsvHelper;
using CsvHelper.Configuration;
using System.Globalization;
using System.IO;
using System.Text;

namespace Bytestrone.AppraisalSystem.Infrastructure.Service;

public class CsvExportService : ICsvExportService
{
    public byte[] GenerateCsv(IEnumerable<AppraiseeAnalysisExportDTO> appraiseeAnalysisData)
    {
        using (var memoryStream = new MemoryStream())
        using (var streamWriter = new StreamWriter(memoryStream, Encoding.UTF8))
        using (var csvWriter = new CsvWriter(streamWriter, new CsvConfiguration(CultureInfo.InvariantCulture)))
        {
            // Writing header
            csvWriter.WriteHeader<AppraiseeAnalysisExportDTO>();
            csvWriter.NextRecord();

            // Writing data
            csvWriter.WriteRecords(appraiseeAnalysisData);

            // Flush and return byte array
            streamWriter.Flush();
            return memoryStream.ToArray();
        }
    }
}
