using System.Reflection;
using EsportClash.Core;
using EsportClash.Core.Shared.Id;

namespace EsportClashAPI;

public static class UseCasesServiceRegistration {
  public static IServiceCollection AddUseCases(this IServiceCollection serviceCollection) {
    
    serviceCollection.AddScoped<IIdProvider, RandomIdProvider>();
    serviceCollection.AddMediatR(cfg => {
      cfg.RegisterServicesFromAssembly(typeof(CoreModule).Assembly);
    });
    
    return serviceCollection;
  }
}