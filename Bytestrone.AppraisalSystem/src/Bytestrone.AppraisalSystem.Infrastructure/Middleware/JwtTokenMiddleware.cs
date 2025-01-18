using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace Bytestrone.AppraisalSystem.Infrastructure.Middleware;

public class JWTAuthenticationMiddleware(RequestDelegate next, IConfiguration configuration)
{
    private readonly RequestDelegate _next = next;
    private readonly IConfiguration _configuration = configuration;

  public async Task InvokeAsync(HttpContext context)
    {
        var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

        if (token != null)
        {
            var secretKey = _configuration.GetValue<string>("JwtSettings:SecretKey")
                            ?? throw new InvalidOperationException("Secret key is not configured.");
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            try
            {
                var handler = new JwtSecurityTokenHandler();
                var tokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,    // Adjust depending on your settings
                    ValidateAudience = false,  // Adjust depending on your settings
                    ValidateLifetime = true,
                    IssuerSigningKey = key,
                };

                var claimsPrincipal = handler.ValidateToken(token, tokenValidationParameters, out var validatedToken);

                // Set the validated claims to HttpContext.User
                context.User = claimsPrincipal;
            }
            catch (SecurityTokenExpiredException)
            {
                context.Response.StatusCode = 401;
                await context.Response.WriteAsync("Token has expired");
                return;
            }
            catch (SecurityTokenValidationException)
            {
                context.Response.StatusCode = 401;
                await context.Response.WriteAsync("Invalid token");
                return;
            }
            catch (Exception)
            {
                context.Response.StatusCode = 401;
                await context.Response.WriteAsync("Unauthorized access");
                return;
            }
        }
        await _next(context);
    }
}
