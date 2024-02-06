using EsportClash.Core.Players.Ports;
using EsportClash.Core.Players.ViewModel;
using EsportClash.Core.Shared;

namespace EsportClash.Core.Players.UseCases.GetAllPlayers;

public class GetAllPlayersUseCase : IUseCase<GetAllPlayersCommand, List<PlayerViewModel>> {
  private readonly IPlayerRepository _playerRepository;
  
  public GetAllPlayersUseCase(IPlayerRepository playerRepository) {
    _playerRepository = playerRepository;
  }
  
  public async Task<List<PlayerViewModel>> Execute(GetAllPlayersCommand request) {
    var players = await _playerRepository.FindAllAsync();
    return players.Select(player => new PlayerViewModel(player)).ToList();
  }
}