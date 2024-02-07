using EsportClash.Core.Players.Ports;
using EsportClash.Core.Shared;
using MediatR;

namespace EsportClash.Core.Players.Commands.DeletePlayer;

public class DeletePlayerCommandHandler : IRequestHandler<DeletePlayerCommand, Unit> {
  private readonly IPlayerRepository _repository;

  public DeletePlayerCommandHandler(IPlayerRepository repository) {
    _repository = repository;
  }

  public async Task<Unit> Handle(DeletePlayerCommand request, CancellationToken token) {
    var player = await _repository.FindByIdAsync(request.Id);
    if (player == null) throw new NotFoundException("Player", request.Id);

    await _repository.DeleteAsync(player);

    return Unit.Value;
  }
}