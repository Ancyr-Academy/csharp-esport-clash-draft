using EsportClash.Core.Players.UseCases.CreatePlayer;
using EsportClash.Core.Players.UseCases.DeletePlayer;
using EsportClash.Core.Players.UseCases.GetAllPlayers;
using EsportClash.Core.Players.UseCases.GetPlayerById;
using EsportClash.Core.Shared.Id;

namespace EsportClashAPI;

public static class UseCasesServiceRegistration {
  public static IServiceCollection AddUseCases(this IServiceCollection serviceCollection) {
    
    serviceCollection.AddScoped<IIdProvider, RandomIdProvider>();
    
    serviceCollection.AddScoped<CreatePlayerUseCase>();
    serviceCollection.AddScoped<DeletePlayerUseCase>();
    serviceCollection.AddScoped<GetPlayerByIdUseCase>();
    serviceCollection.AddScoped<GetAllPlayersUseCase>();
    
    return serviceCollection;
  }
}