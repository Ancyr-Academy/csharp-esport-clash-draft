using System.Data.Common;
using EsportClash.Persistence.SQL;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EsportClash.APITests;

public class WebTestsFixture : WebApplicationFactory<Program> {
  private readonly DatabaseSetup _databaseSetup = new DatabaseSetup();
  
  protected override void ConfigureWebHost(IWebHostBuilder builder) {
    builder.UseEnvironment("Testing");
    builder.ConfigureServices(services => {
      services.Remove(services.SingleOrDefault(service => typeof(DbContextOptions<EsportDatabaseContext>) == service.ServiceType));
      services.Remove(services.SingleOrDefault(service => typeof(DbConnection) == service.ServiceType));
      services.AddDbContext<EsportDatabaseContext>(options => {
        options.UseNpgsql(_databaseSetup.ConnectionString());
      });

      var serviceProvider = services.BuildServiceProvider();

      using (var scope = serviceProvider.CreateScope()) {
        var db = scope.ServiceProvider.GetRequiredService<EsportDatabaseContext>();
        db.Database.Migrate();
      }
    });
    
  }
}