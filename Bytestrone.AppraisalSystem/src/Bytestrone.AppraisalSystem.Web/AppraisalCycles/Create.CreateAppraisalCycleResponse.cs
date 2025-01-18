namespace Bytestrone.AppraisalSystem.web.AppraisalCycles;
public class CreateAppraisalCycleResponse(int id, string message)
{
  public int Id { get; set; } = id;
  public string Message { get; set; }=message;
}
