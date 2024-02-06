using EsportClash.Core.Players.Ports;
using EsportClash.Core.Players.ViewModel;
using EsportClash.Core.Shared;
using MediatR;

namespace EsportClash.Core.Players.Queries.GetAllPlayers;

public class GetAllPlayersUseCase : IRequestHandler<GetAllPlayersQuery, List<PlayerViewModel>> {
  private readonly IPlayerRepository _playerRepository;
  
  public GetAllPlayersUseCase(IPlayerRepository playerRepository) {
    _playerRepository = playerRepository;
  }
  
  public async Task<List<PlayerViewModel>> Handle(GetAllPlayersQuery request, CancellationToken token) {
    var players = await _playerRepository.FindAllAsync();
    return players.Select(player => new PlayerViewModel(player)).ToList();
  }
}