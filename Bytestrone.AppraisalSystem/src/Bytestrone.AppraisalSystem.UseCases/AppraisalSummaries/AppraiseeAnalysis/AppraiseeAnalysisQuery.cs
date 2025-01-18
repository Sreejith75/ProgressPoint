using Ardalis.Result;
using Ardalis.SharedKernel;

namespace Bytestrone.AppraisalSystem.UseCases.AppraisalSummaries.AppraiseeAnalysis;
public record AppraiseeAnalysisQuery(int? QuarterId, int Year, int? DepartmentId, int? RoleId) : IQuery<Result<List<AppraiseeAnalysisDTO>>>;