using EsportClash.Core.Players.Commands.CreatePlayer;
using EsportClash.Core.Players.Commands.DeletePlayer;
using EsportClash.Core.Players.Queries.GetAllPlayers;
using EsportClash.Core.Players.Queries.GetPlayerById;
using EsportClash.Core.Players.ViewModel;
using EsportClash.Core.Shared;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EsportClashAPI.Players;

[ApiController]
[Route("/players")]
public class PlayersController : ControllerBase {
  private readonly IMediator _mediator;


  public PlayersController(IMediator mediator) {
    _mediator = mediator;
  }

  [HttpGet]
  public Task<List<PlayerViewModel>> GetAllPlayers() {
    return _mediator.Send(new GetAllPlayersQuery());
  }

  [HttpGet("{id}")]
  public async Task<ActionResult<PlayerViewModel>> Get(string id) {
    var player = await _mediator.Send(new GetPlayerByIdQuery { Id = id });
    return Ok(player);
  }

  [HttpPost]
  public async Task<ActionResult<IdResponse>> Create([FromBody] CreatePlayerCommand command) {
    var response = await _mediator.Send(command);
    return CreatedAtAction(nameof(Get), new { id = response.Id }, response);
  }

  [HttpDelete("{id}")]
  public async Task<ActionResult> Delete(string id) {
    await _mediator.Send(new DeletePlayerCommand { Id = id });
    return NoContent();
  }
}