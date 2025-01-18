
using Ardalis.Result;
using Ardalis.SharedKernel;
using Bytestrone.AppraisalSystem.Core.Entities.AppraisalCycleAggregate;
namespace Bytestrone.AppraisalSystem.UseCases.AppraisalCycles.ListQuarterYear;
public class ListQuarterYearQuery: IQuery<Result<QuarterYearDTO>>;