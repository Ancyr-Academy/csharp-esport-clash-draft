using EsportClash.Identity.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EsportClash.Identity.DbContext;

public class AppIdentityDbContext : IdentityDbContext<AppUser> {
  public AppIdentityDbContext(DbContextOptions<AppIdentityDbContext> options) : base(options) { }

  protected override void OnModelCreating(ModelBuilder builder) {
    base.OnModelCreating(builder);
    builder.ApplyConfigurationsFromAssembly(typeof(AppIdentityDbContext).Assembly);
  }
}