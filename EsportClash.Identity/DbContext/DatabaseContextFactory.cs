using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace EsportClash.Identity.DbContext;

public class DatabaseContextFactory : IDesignTimeDbContextFactory<AppIdentityDbContext> {
  public AppIdentityDbContext CreateDbContext(string[] args) {
    var configuration = new ConfigurationBuilder()
      .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
      .AddJsonFile("appsettings.json")
      .Build();

    var optionsBuilder = new DbContextOptionsBuilder<AppIdentityDbContext>();
    optionsBuilder.UseNpgsql(configuration.GetConnectionString("EsportClashDatabaseConnectionString"));

    return new AppIdentityDbContext(optionsBuilder.Options);
  }
}