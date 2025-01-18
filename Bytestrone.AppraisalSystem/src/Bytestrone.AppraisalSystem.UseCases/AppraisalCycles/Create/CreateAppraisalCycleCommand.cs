using Ardalis.Result;
using Ardalis.SharedKernel;
using Bytestrone.AppraisalSystem.Core.Entities.AppraisalCycleAggregate;
using MediatR;
using System;
using System.Windows.Input;

namespace Bytestrone.AppraisalSystem.UseCases.AppraisalCycles.Create;
public class CreateAppraisalCycleCommand(int quarter, int year, DateOnly appraiseeStartDate, DateOnly appraiseeEndDate, DateOnly appraiserStartDate, DateOnly appraiserEndDate) : ICommand<Result<int>>
{
  public int Quarter { get; set; } = quarter;
  public int Year { get; set; } = year;
  public DateOnly AppraiseeStartDate { get; set; } = appraiseeStartDate;
  public DateOnly AppraiseeEndDate { get; set; } = appraiseeEndDate;
  public DateOnly AppraiserStartDate { get; set; } = appraiserStartDate;
  public DateOnly AppraiserEndDate { get; set; } = appraiserEndDate;
}
