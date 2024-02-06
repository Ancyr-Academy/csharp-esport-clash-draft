using EsportClash.Core.Players.Ports;
using EsportClash.Core.Shared;
using EsportClash.Core.Shared.Id;

namespace EsportClash.Core.Players.UseCases.CreatePlayer;

public class CreatePlayerUseCase : IUseCase<CreatePlayerCommand, IdResponse> {
  private readonly IIdProvider _idProvider;
  private readonly IPlayerRepository _playerRepository;
  
  public CreatePlayerUseCase(IIdProvider idProvider, IPlayerRepository playerRepository) {
    _idProvider = idProvider;
    _playerRepository = playerRepository;
  }
  
  public async Task<IdResponse> Execute(CreatePlayerCommand request) {
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