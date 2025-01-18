using Ardalis.Result;
using Ardalis.SharedKernel;

namespace Bytestrone.AppraisalSystem.UseCases.AppraisalSummaries.ExportSummery;
public record ExportSummeryQuery(int? QuarterId, int Year, int? DepartmentId, int? RoleId) : IQuery<Result<byte[]>>;