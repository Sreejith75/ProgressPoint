using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Bytestrone.AppraisalSystem.UseCases.Interface;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;

namespace Bytestrone.AppraisalSystem.Infrastructure.Service;
public class JwtTokenService(IConfiguration configuration) : IJwtTokenService
{
    private readonly IConfiguration _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));

  public string GenerateToken(string userId, string username, IEnumerable<string> roles, List<string> permissions, int EmployeeRoleId)
    {
        var jwtSettings = _configuration.GetSection("JwtSettings");
        var secretKey = _configuration.GetValue<string>("JwtSettings:SecretKey") ?? throw new InvalidOperationException("Secret key is not configured.");
        var expirationMinutes = jwtSettings.GetValue<int>("ExpirationMinutes", 60);  

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var claims = new List<Claim>
            {
                new(JwtRegisteredClaimNames.Sub, userId),
                new(JwtRegisteredClaimNames.UniqueName, username),
                new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new(JwtRegisteredClaimNames.Iat, new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds().ToString(), ClaimValueTypes.Integer64)
            };

        if (roles != null)
        {
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }
        }

        claims.Add(new Claim("employee_role_id", EmployeeRoleId.ToString()));
        claims.Add(new Claim("roles", JsonConvert.SerializeObject(roles ?? Enumerable.Empty<string>())));
        claims.Add(new Claim("user_permissions", JsonConvert.SerializeObject(permissions ?? new List<string>())));

        var token = new JwtSecurityToken(
            claims: claims,
            notBefore: DateTime.UtcNow,
            expires: DateTime.UtcNow.AddMinutes(expirationMinutes),
            signingCredentials: credentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}