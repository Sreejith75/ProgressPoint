namespace Bytestrone.AppraisalSystem.Core.Interfaces;
public interface IHashingService
{
    string HashPassword(string password);
    bool VerifyPassword(string hashedPassword, string password);
}