using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace EsportClash.Persistence.SQL;

public class DatabaseContextFactory : IDesignTimeDbContextFactory<EsportDatabaseContext> {
  public EsportDatabaseContext CreateDbContext(string[] args)
  {
    IConfigurationRoot configuration = new ConfigurationBuilder()
      .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
      .AddJsonFile("appsettings.json")
      .Build();
    
    var optionsBuilder = new DbContextOptionsBuilder<EsportDatabaseContext>();
    optionsBuilder.UseNpgsql(configuration.GetConnectionString("EsportClashDatabaseConnectionString"));
      
    return new EsportDatabaseContext(optionsBuilder.Options);
  }
}