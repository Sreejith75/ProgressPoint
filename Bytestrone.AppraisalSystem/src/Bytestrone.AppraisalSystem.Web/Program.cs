using System.Text;
using Bytestrone.AppraisalSystem.Infrastructure.Middleware;
using Bytestrone.AppraisalSystem.Web.Configurations;
using FastEndpoints;
using FastEndpoints.Swagger;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using Serilog.Extensions.Logging;

var builder = WebApplication.CreateBuilder(args);

var logger = Log.Logger = new LoggerConfiguration()
  .Enrich.FromLogContext()
  .WriteTo.Console()
  .CreateLogger();

logger.Information("Starting web host");

builder.AddLoggerConfigs();

var appLogger = new SerilogLoggerFactory(logger)
    .CreateLogger<Program>();

builder.Services.AddOptionConfigs(builder.Configuration, appLogger, builder);
builder.Services.AddServiceConfigs(appLogger, builder);
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtSettings:SecretKey"]!))
    };

});
// builder.Services.Configure<FormOptions>(options =>
// {
//     options.MultipartBodyLengthLimit = 50 * 1024 * 1024; // 50 MB limit
// });
builder.Services.AddAuthorization();

// Set up CORS policy with the correct name
builder.Services.AddCors(options =>
{
  options.AddPolicy("AllowSpecificOrigins", policy =>
  {
    policy.WithOrigins("http://localhost:3000") 
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowAnyHeader();
  });
});



// Register FastEndpoints and Swagger
builder.Services.AddFastEndpoints()
    .SwaggerDocument(o =>
    {
        o.ShortSchemaNames = true;
    });


// Build the app
var app = builder.Build();

// Apply CORS policy in the correct order before routing
app.UseCors("AllowSpecificOrigins");
app.UseAuthentication();
// Custom middleware (ensure it's implemented properly)
app.UseMiddleware<JWTAuthenticationMiddleware>();
app.UseAuthorization();
await app.UseAppMiddleware();

// Start the app
app.Run();

// Make the implicit Program.cs class public for integration testing
public partial class Program { }
