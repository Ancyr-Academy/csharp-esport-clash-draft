using EsportClash.Core.Players.Ports;
using EsportClash.Core.Players.ViewModel;
using EsportClash.Core.Shared;
using MediatR;

namespace EsportClash.Core.Players.Queries.GetPlayerById;

public class GetPlayerByIdUseCase : IRequestHandler<GetPlayerByIdQuery, PlayerViewModel> {
  private readonly IPlayerRepository _playerRepository;
  
  public GetPlayerByIdUseCase(IPlayerRepository playerRepository) {
    _playerRepository = playerRepository;
  }
  
  public async Task<PlayerViewModel> Handle(GetPlayerByIdQuery request, CancellationToken token) {
    var player = await _playerRepository.FindByIdAsync(request.Id);
    if (player == null) {
      throw new NotFoundException("Player", request.Id);
    }

    return new PlayerViewModel(player);
  }
}