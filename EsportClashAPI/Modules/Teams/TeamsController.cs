using EsportClash.Core.Shared;
using EsportClash.Core.Teams.Commands.AddPlayerToTeam;
using EsportClash.Core.Teams.Commands.CreateTeam;
using EsportClash.Core.Teams.Commands.DeleteTeam;
using EsportClash.Core.Teams.Commands.RemovePlayerFromTeam;
using EsportClash.Core.Teams.Queries.GetAllTeams;
using EsportClash.Core.Teams.Queries.GetTeamById;
using EsportClash.Core.Teams.ViewModel;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EsportClashAPI.Modules.Teams;

[ApiController]
[Route("/teams")]
public class TeamsController : ControllerBase {
  private readonly IMediator _mediator;

  public TeamsController(IMediator mediator) {
    _mediator = mediator;
  }

  [HttpGet]
  public Task<List<TeamViewModel>> GetAllTeams() {
    return _mediator.Send(new GetAllTeamsQuery());
  }

  [HttpGet("{id}")]
  public Task<TeamViewModel> GetTeam(string id) {
    return _mediator.Send(new GetTeamByIdQuery { Id = id });
  }

  [HttpPost]
  public Task<IdResponse> CreateTeam([FromBody] CreateTeamCommand command) {
    return _mediator.Send(command);
  }

  [HttpDelete("{id}")]
  public Task Delete(string id) {
    return _mediator.Send(new DeleteTeamCommand { Id = id });
  }

  [HttpPost("add-player-to-team")]
  public Task AddPlayerToTeam([FromBody] AddPlayerToTeamCommand command) {
    return _mediator.Send(command);
  }

  [HttpDelete("remove-player-from-team")]
  public Task RemovePlayerFromTeam([FromBody] RemovePlayerFromTeamCommand command) {
    return _mediator.Send(command);
  }
}