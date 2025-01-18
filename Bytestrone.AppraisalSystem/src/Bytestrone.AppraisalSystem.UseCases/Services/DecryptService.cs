using System.Security.Cryptography;
using System.Text;
namespace Bytestrone.AppraisalSystem.UseCases.Services;
public class DecryptService
{
    private const string SecretKey = "AIzaSyA8z0AxA0BAxwK-ZL7OTruPy090cxkrFVQ";
    private static readonly byte[] IV = new byte[16];

    public static string Decrypt(string encryptedText)
    {
        var key = Encoding.UTF8.GetBytes(SecretKey);

        using (var aes = Aes.Create())
        {
            aes.Key = key;
            aes.IV = IV;

            aes.Mode = CipherMode.CBC;
            aes.Padding = PaddingMode.PKCS7;

            var decryptor = aes.CreateDecryptor(aes.Key, aes.IV);
            var buffer = Convert.FromBase64String(encryptedText);

            using (var ms = new MemoryStream(buffer))
            using (var cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
            using (var sr = new StreamReader(cs))
            {
                return sr.ReadToEnd();
            }
        }
    }
}