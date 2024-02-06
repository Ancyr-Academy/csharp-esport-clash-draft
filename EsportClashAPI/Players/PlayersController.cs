using EsportClash.Core.Players.UseCases.CreatePlayer;
using EsportClash.Core.Players.UseCases.DeletePlayer;
using EsportClash.Core.Players.UseCases.GetAllPlayers;
using EsportClash.Core.Players.UseCases.GetPlayerById;
using EsportClash.Core.Players.ViewModel;
using EsportClash.Core.Shared;
using Microsoft.AspNetCore.Mvc;

namespace EsportClashAPI.Players;

[ApiController]
[Route("/players")]
public class PlayersController : ControllerBase {
  private readonly GetAllPlayersUseCase _getAllPlayersUseCase;
  private readonly GetPlayerByIdUseCase _getPlayerByIdUseCase;
  private readonly CreatePlayerUseCase _createPlayerUseCase;
  private readonly DeletePlayerUseCase _deletePlayerUseCase;

  public PlayersController(
    GetAllPlayersUseCase getAllPlayersUseCase,
    GetPlayerByIdUseCase getPlayerByIdUseCase,
    CreatePlayerUseCase createPlayerUseCase,
    DeletePlayerUseCase deletePlayerUseCase
  ) {
    _getAllPlayersUseCase = getAllPlayersUseCase;
    _getPlayerByIdUseCase = getPlayerByIdUseCase;
    _createPlayerUseCase = createPlayerUseCase;
    _deletePlayerUseCase = deletePlayerUseCase;
  }

  [HttpGet]
  public Task<List<PlayerViewModel>> GetAllPlayers() {
    return _getAllPlayersUseCase.Execute(new GetAllPlayersCommand());
  }
  
  [HttpGet("{id}")]
  public async Task<ActionResult<PlayerViewModel>> Get(String id)
  {
    var player = await _getPlayerByIdUseCase.Execute(new GetPlayerByIdCommand { Id = id });
    return Ok(player);
  }
  
  [HttpPost]
  public async Task<ActionResult<IdResponse>> Create([FromBody] CreatePlayerCommand command)
  {
    var response = await _createPlayerUseCase.Execute(command);
    return CreatedAtAction(nameof(Get), new { id = response.Id }, response);
  }
  
  [HttpDelete("{id}")]
  public async Task<ActionResult> Delete(String id)
  {
    await _deletePlayerUseCase.Execute(new DeletePlayerCommand { Id = id });
    return NoContent();
  }
}