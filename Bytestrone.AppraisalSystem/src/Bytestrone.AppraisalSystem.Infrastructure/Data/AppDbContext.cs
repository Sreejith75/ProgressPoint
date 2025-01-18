using System.Reflection;
using Ardalis.SharedKernel;
using Bytestrone.AppraisalSystem.Core.ContributorAggregate;
using Bytestrone.AppraisalSystem.Core.EmployeeAggregate;
using Bytestrone.AppraisalSystem.Core.Entities.AppraisalCycleAggregate;
using Bytestrone.AppraisalSystem.Core.Entities.AppraisalFeedbackAggregate;
using Bytestrone.AppraisalSystem.Core.Entities.AppraisalFormAggregate;
using Bytestrone.AppraisalSystem.Core.Entities.AppraisalSummeryAggregate;
using Bytestrone.AppraisalSystem.Core.Entities.DepartmentAggregate;
using Bytestrone.AppraisalSystem.Core.Entities.EmployeeAggregate;
using Bytestrone.AppraisalSystem.Core.Entities.EmployeeRoleAggregate;
using Bytestrone.AppraisalSystem.Core.Entities.PerformanceFactorAggregate;
using Bytestrone.AppraisalSystem.Core.Entities.PerformanceIndicatorAggregate;
using Bytestrone.AppraisalSystem.Core.Entities.QuestionAggregate;
using Bytestrone.AppraisalSystem.Core.PermissionAggregate;
using Bytestrone.AppraisalSystem.Core.SystemRoleAggregate;
using Microsoft.EntityFrameworkCore;

namespace Bytestrone.AppraisalSystem.Infrastructure.Data;
public class AppDbContext(DbContextOptions<AppDbContext> options,
  IDomainEventDispatcher? dispatcher) : DbContext(options)
{
  private readonly IDomainEventDispatcher? _dispatcher = dispatcher;

  public DbSet<Contributor> Contributors => Set<Contributor>();

  //Employee Aggregate
  public DbSet<Employee> Employees => Set<Employee>();
  public DbSet<EmployeeSystemRole> EmployeeSystemRoles => Set<EmployeeSystemRole>();
  public DbSet<EmployeeAppraiserMapping> EmployeeAppraiserMappings => Set<EmployeeAppraiserMapping>();

  //EmployeeRole Aggregate
  public DbSet<EmployeeRole> EmployeeRoles => Set<EmployeeRole>();

  //SystemRole Aggregate
  public DbSet<SystemRole> SystemRoles => Set<SystemRole>();
  public DbSet<Permission> Permissions => Set<Permission>();
  public DbSet<SystemRolePermission> SystemRolePermissionMappings => Set<SystemRolePermission>();

  //PerformanceFactor Aggregate
  public DbSet<PerformanceFactor> PerformanceFactors => Set<PerformanceFactor>();
  public DbSet<PerformanceIndicator> PerformanceIndicators => Set<PerformanceIndicator>();
  public DbSet<Question> Question => Set<Question>();

  //RolePerformanceFactor Aggregate
  public DbSet<DepartmentPerformanceFactor> rolePerformanceFactors => Set<DepartmentPerformanceFactor>();

  //AppraisalCycle Aggregate
  public DbSet<AppraisalCycle> appraisalCycles => Set<AppraisalCycle>();

  //AppraisalForm Aggregate
  public DbSet<AppraisalForm> appraisalForm => Set<AppraisalForm>();
  public DbSet<FormQuestion> formQuestion => Set<FormQuestion>();

  //AppraisalFeedbackAggregate
  public DbSet<AppraisalFeedback> appraisalFeedbacks => Set<AppraisalFeedback>();
  public DbSet<AppraisalFeedbackDetail> appraisalFeedbackDetails => Set<AppraisalFeedbackDetail>();

  //Department Aggregate
  public DbSet<Department> departments => Set<Department>();

  //AppraisalSummery Aggregate
  public DbSet<AppraisalSummary> appraisalSummaries => Set<AppraisalSummary>();


  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    base.OnModelCreating(modelBuilder);
    modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
  }

  public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
  {
    int result = await base.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

    // ignore events if no dispatcher provided
    if (_dispatcher == null) return result;

    // dispatch events only if save was successful
    var entitiesWithEvents = ChangeTracker.Entries<EntityBase>()
        .Select(e => e.Entity)
        .Where(e => e.DomainEvents.Any())
        .ToArray();

    await _dispatcher.DispatchAndClearEvents(entitiesWithEvents);

    return result;
  }

  public override int SaveChanges() =>
        SaveChangesAsync().GetAwaiter().GetResult();
}
