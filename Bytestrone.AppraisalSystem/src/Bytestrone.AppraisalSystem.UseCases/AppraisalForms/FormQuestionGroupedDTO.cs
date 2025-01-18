using Bytestrone.AppraisalSystem.UseCases.PerformanceFactors;
namespace Bytestrone.AppraisalSystem.UseCases.AppraisalForms;
public class FormQuestionGroupedDTO
{
    public int FormId { get; set; }
    public List<PerformanceFactorsDTO> Factors { get; set; } = new();
}