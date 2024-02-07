using EsportClash.Core.Players.Ports;
using EsportClash.Core.Players.ViewModel;
using EsportClash.Core.Shared;
using MediatR;

namespace EsportClash.Core.Players.Queries.GetAllPlayers;

public class GetAllPlayersQueryHandler : IRequestHandler<GetAllPlayersQuery, List<PlayerViewModel>> {
  private readonly IPlayerRepository _playerRepository;
  
  public GetAllPlayersQueryHandler(IPlayerRepository playerRepository) {
    _playerRepository = playerRepository;
  }
  
  public async Task<List<PlayerViewModel>> Handle(GetAllPlayersQuery request, CancellationToken token) {
    var players = await _playerRepository.FindAllAsync();
    return players.Select(player => new PlayerViewModel {
      Id = player.Id,
      Name = player.Name,
      Role = player.MainRole.ToString()
    }).ToList();
  }
}