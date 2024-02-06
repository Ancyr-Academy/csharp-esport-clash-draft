using EsportClash.Core.Players.Model;
using EsportClash.Core.Shared;
using EsportClash.Core.Teams.Model;
using Microsoft.EntityFrameworkCore;

namespace EsportClash.Persistence.SQL;

public class EsportDatabaseContext : DbContext {
  public DbSet<Player> Players { get; set; }
  public DbSet<Team> Teams { get; set; }
  
  public EsportDatabaseContext(DbContextOptions<EsportDatabaseContext> options) : base(options) {
    
  }

  protected override void OnModelCreating(ModelBuilder modelBuilder) {
    modelBuilder.ApplyConfigurationsFromAssembly(typeof (EsportDatabaseContext).Assembly);
    base.OnModelCreating(modelBuilder);
  }

  public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default) {
    foreach (var entry in base.ChangeTracker.Entries<BaseEntity>().Where(
               q => q.State == EntityState.Added || q.State == EntityState.Modified
               )) {
      entry.Entity.UpdatedAt = DateTime.Now.ToUniversalTime();
      if (entry.State == EntityState.Added) {
        entry.Entity.CreatedAt = DateTime.Now.ToUniversalTime();
      }
    }
    
    return base.SaveChangesAsync(cancellationToken);
  }
}