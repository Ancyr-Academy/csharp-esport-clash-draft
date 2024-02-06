using EsportClash.Core.Players.Ports;
using EsportClash.Core.Shared;
using EsportClash.Core.Shared.Id;
using MediatR;

namespace EsportClash.Core.Players.Commands.CreatePlayer;

public class CreatePlayerCommandHandler : IRequestHandler<CreatePlayerCommand, IdResponse> {
  private readonly IIdProvider _idProvider;
  private readonly IPlayerRepository _playerRepository;
  
  public CreatePlayerCommandHandler(IIdProvider idProvider, IPlayerRepository playerRepository) {
    _idProvider = idProvider;
    _playerRepository = playerRepository;
  }
  
  
  public async Task<IdResponse> Handle(CreatePlayerCommand request, CancellationToken cancellationToken) {
    var validator = new CreatePlayerCommandValidator();
    var validationResult = await validator.ValidateAsync(request);
    
    if (validationResult.Errors.Any()) {
      throw new BadRequestException("Invalid input", validationResult);
    }
    
    var player = new Model.Player {
      Id = _idProvider.NewId(),
      Name = request.Name,
      MainRole = request.MainRole
    };

    await _playerRepository.CreateAsync(player);
    
    return new IdResponse(player.Id);
  }
}