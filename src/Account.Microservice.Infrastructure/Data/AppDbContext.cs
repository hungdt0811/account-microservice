using System.Reflection;
using Account.Microservice.Core.Entities.CoursesAggreate;
using Account.Microservice.Core.Entities.SecurityAggregate;
using Account.Microservice.Core.Entities.UserAggregate;
using Account.Microservice.Core.ProjectAggregate;
using Account.Microservice.SharedKernel;
using Account.Microservice.SharedKernel.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;

namespace Account.Microservice.Infrastructure.Data;

public class AppDbContext : DbContext
{
  private readonly IDomainEventDispatcher? _dispatcher;

  public AppDbContext(DbContextOptions<AppDbContext> options,
    IDomainEventDispatcher? dispatcher)
      : base(options)
  {
    _dispatcher = dispatcher;
  }
  public DbSet<User> Users => Set<User>();
  public DbSet<Role> Role => Set<Role>();
  public DbSet<RolePermission> RolePermission => Set<RolePermission>();
  public DbSet<Permission> Permission => Set<Permission>();
  public DbSet<UserRole> UserRole => Set<UserRole>();
  public DbSet<Course> Course => Set<Course>();

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    base.OnModelCreating(modelBuilder);
    modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    //foreach (var property in modelBuilder.Model.GetEntityTypes()
    //             .SelectMany(t => t.GetProperties())
    //             .Where
    //             (p
    //               => p.ClrType == typeof(DateTime)
    //                  || p.ClrType == typeof(DateTime?)
    //             )
    //    )
    //{
    //  property.SetColumnType("timestamp without time zone");
    //}
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

  public override int SaveChanges()
  {
    return SaveChangesAsync().GetAwaiter().GetResult();
  }
}
