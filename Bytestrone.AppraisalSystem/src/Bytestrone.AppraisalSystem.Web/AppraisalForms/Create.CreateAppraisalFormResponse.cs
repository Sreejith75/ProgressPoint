namespace Bytestrone.AppraisalSystem.web.AppraisalForms;
public record CreateAppraisalFormResponse(int Id, string Message)
{
    public int Id { get; set; }=Id;
    public string Message { get; set; } = Message;
}