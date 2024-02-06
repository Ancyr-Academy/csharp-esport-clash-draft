using EsportClash.Core.Players.Ports;
using EsportClash.Core.Shared;
using EsportClash.Core.Teams.Ports;
using EsportClash.Persistence.SQL.Modules.Players;
using EsportClash.Persistence.SQL.Modules.Teams;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EsportClash.Persistence.SQL;

public static class PersistenceServiceRegistration {
  public static IServiceCollection AddPersistenceServices(this IServiceCollection services,
    IConfiguration configuration) {
    services.AddDbContext<EsportDatabaseContext>(options => {
      options.UseNpgsql(configuration.GetConnectionString("EsportClashDatabaseConnectionString"));
    });
    
    services.AddScoped<IPlayerRepository, SqlPlayerRepository>();
    services.AddScoped<ITeamRepository, SqlTeamRepository>();
    
    return services;
  }
}