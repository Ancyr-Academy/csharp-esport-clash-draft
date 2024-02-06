
using Microsoft.EntityFrameworkCore;
using Npgsql;
using Testcontainers.PostgreSql;

namespace EsportClash.Persistence.SQL.Tests;

public class DatabaseSetup : IDisposable {
  private readonly PostgreSqlContainer _postgreSqlContainer = new PostgreSqlBuilder().WithImage("postgres:13").Build();
  public EsportDatabaseContext Context { get; }
  
  public DatabaseSetup() {
    _postgreSqlContainer.StartAsync().Wait();
     
    var dbOptions = new DbContextOptionsBuilder<EsportDatabaseContext>();
    var csBuilder = new NpgsqlConnectionStringBuilder(_postgreSqlContainer.GetConnectionString());
    csBuilder.IncludeErrorDetail = true;
    
    dbOptions.UseNpgsql(csBuilder.ConnectionString);
     
    Context = new EsportDatabaseContext(dbOptions.Options);
    Context.Database.MigrateAsync().Wait();
  }

  public void Dispose() {
    _postgreSqlContainer.DisposeAsync().AsTask().Wait();
  }
}