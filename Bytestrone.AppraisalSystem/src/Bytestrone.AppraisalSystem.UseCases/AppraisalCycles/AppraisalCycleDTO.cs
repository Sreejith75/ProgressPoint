using Bytestrone.AppraisalSystem.Core.Entities.AppraisalCycleAggregate;

namespace Bytestrone.AppraisalSystem.UseCases.AppraisalCycles;
public record AppraisalCycleDTO 
{
    public int Id { get; set; }
    public string? Quarter { get; set; }
    public int Year { get; set; }
    public DateOnly AppraiseeStartDate { get; set; }
    public DateOnly AppraiseeEndDate { get; set; }
    public DateOnly AppraiserStartDate { get; set; }
    public DateOnly AppraiserEndDate { get; set; }
    public string? Status { get; set; }
}