using EsportClash.Core.Players.Ports;
using EsportClash.Core.Players.ViewModel;
using EsportClash.Core.Shared;

namespace EsportClash.Core.Players.UseCases.GetPlayerById;

public class GetPlayerByIdUseCase : IUseCase<GetPlayerByIdCommand, PlayerViewModel> {
  private readonly IPlayerRepository _playerRepository;
  
  public GetPlayerByIdUseCase(IPlayerRepository playerRepository) {
    _playerRepository = playerRepository;
  }
  
  public async Task<PlayerViewModel> Execute(GetPlayerByIdCommand request) {
    var player = await _playerRepository.FindByIdAsync(request.Id);
    if (player == null) {
      throw new NotFoundException("Player", request.Id);
    }

    return new PlayerViewModel(player);
  }
}