
using EsportClash.Persistence.SQL;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using Testcontainers.PostgreSql;

namespace EsportClash.APITests;

public class DatabaseSetup : IDisposable {
  private readonly PostgreSqlContainer _postgreSqlContainer = new PostgreSqlBuilder().WithImage("postgres:13").Build();

  public string ConnectionString() {
    return _postgreSqlContainer.GetConnectionString();
  }
  
  public DatabaseSetup() {
    _postgreSqlContainer.StartAsync().Wait();
  }

  public void Dispose() {
    _postgreSqlContainer.DisposeAsync().AsTask().Wait();
  }
}