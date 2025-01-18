namespace Bytestrone.AppraisalSystem.UseCases.PerformanceFactors.List;
public interface IListPerformanceFactorsQueryService
{
  Task<IEnumerable<PerformanceFactorWithIndicatorsDTO>> ListAsync();
}
