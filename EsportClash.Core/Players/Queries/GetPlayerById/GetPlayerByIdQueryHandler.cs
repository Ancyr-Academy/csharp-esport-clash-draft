using EsportClash.Core.Players.Ports;
using EsportClash.Core.Players.ViewModel;
using EsportClash.Core.Shared;
using MediatR;

namespace EsportClash.Core.Players.Queries.GetPlayerById;

public class GetPlayerByIdQueryHandler : IRequestHandler<GetPlayerByIdQuery, PlayerViewModel> {
  private readonly IPlayerRepository _playerRepository;
  
  public GetPlayerByIdQueryHandler(IPlayerRepository playerRepository) {
    _playerRepository = playerRepository;
  }
  
  public async Task<PlayerViewModel> Handle(GetPlayerByIdQuery request, CancellationToken token) {
    var player = await _playerRepository.FindByIdAsync(request.Id);
    if (player == null) {
      throw new NotFoundException("Player", request.Id);
    }

    return new PlayerViewModel {
      Id = player.Id,
      Name = player.Name,
      Role = player.MainRole.ToString()
    };
  }
}