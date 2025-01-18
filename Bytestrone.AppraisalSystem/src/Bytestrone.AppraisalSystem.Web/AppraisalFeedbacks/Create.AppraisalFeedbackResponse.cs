using Org.BouncyCastle.Pqc.Crypto.Ntru;

namespace Bytestrone.AppraisalSystem.web.AppraisalFeedbacks;
public class AppraisalFeedbackResponse(int Id, bool Message)
{
    public int Id { get; set; } = Id;
    public bool Message { get; set; } = Message;
}