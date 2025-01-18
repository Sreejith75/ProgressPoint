using Ardalis.GuardClauses;
using Ardalis.SharedKernel;
using Bytestrone.AppraisalSystem.Core.Interfaces;
using Bytestrone.AppraisalSystem.Core.Services;
using Bytestrone.AppraisalSystem.Infrastructure.Data;
using Bytestrone.AppraisalSystem.Infrastructure.Data.Queries;
using Bytestrone.AppraisalSystem.Infrastructure.Service;
using Bytestrone.AppraisalSystem.UseCases.Contributors.List;
using Bytestrone.AppraisalSystem.UseCases.Interface;
using Bytestrone.AppraisalSystem.UseCases.PerformanceFactors.List;
using Bytestrone.AppraisalSystem.UseCases.Permissions.List;
using Bytestrone.AppraisalSystem.UseCases.Services;
using Bytestrone.AppraisalSystem.UseCases.SystemRoles.List;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Bytestrone.AppraisalSystem.Infrastructure;
public static class InfrastructureServiceExtensions
{
  public static IServiceCollection AddInfrastructureServices(
    this IServiceCollection services,
    ConfigurationManager config,
    ILogger logger)
  {
    string? connectionString = config.GetConnectionString("DefaultConnection");
    Guard.Against.Null(connectionString);
    services.AddDbContext<AppDbContext>(options =>
     options.UseNpgsql(connectionString));
     


    _ = services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>))
           .AddScoped(typeof(IReadRepository<>), typeof(EfRepository<>))
           .AddScoped<IListContributorsQueryService, ListContributorsQueryService>()
           .AddScoped<IDeleteContributorService, DeleteContributorService>()
           .AddScoped<IHashingService, HashingService>()
           .AddScoped<ICreateEmployeeService, CreateEmployeeService>()
           .AddScoped<IJwtTokenService, JwtTokenService>()
           .AddScoped<ILoginService,LoginService>()
           .AddScoped<IHashingService,HashingService>()
           .AddScoped<DecryptService>()
           .AddScoped<IListSystemRoleQueryService,ListSystemRoleQueryService>()
           .AddScoped<IListPermissionQueryService, ListPermissionQueryService>()
           .AddScoped<IListPerformanceFactorsQueryService,ListPerformanceFactorsQueryService>()
           .AddScoped<IArtifactStorageService,ArtifactStorageService>()
           .AddScoped<ICsvExportService,CsvExportService>();
;

    logger.LogInformation("{Project} services registered", "Infrastructure");

    return services;
  }
}
