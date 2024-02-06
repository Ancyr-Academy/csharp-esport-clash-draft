using EsportClash.Core.Players.Ports;
using EsportClash.Core.Shared;

namespace EsportClash.Core.Players.UseCases.DeletePlayer;

public class DeletePlayerUseCase : IUseCase<DeletePlayerCommand, object> {
  private IPlayerRepository _repository;
  
  public DeletePlayerUseCase(IPlayerRepository repository) {
    this._repository = repository;
  }
  
  public async Task<object> Execute(DeletePlayerCommand request) {
    var player = await _repository.FindByIdAsync(request.Id);
    if (player == null) {
      throw new NotFoundException("Player", request.Id);
    }
    
    await _repository.DeleteAsync(player);
    
    return null;
  }
}