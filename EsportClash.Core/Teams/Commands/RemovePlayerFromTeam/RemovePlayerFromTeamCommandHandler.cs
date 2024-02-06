using EsportClash.Core.Players.Ports;
using EsportClash.Core.Shared;
using EsportClash.Core.Teams.Commands.AddPlayerToTeam;
using EsportClash.Core.Teams.Ports;
using MediatR;

namespace EsportClash.Core.Teams.Commands.RemovePlayerFromTeam;

public class RemovePlayerFromTeamCommandHandler : IRequestHandler<RemovePlayerFromTeamCommand, Unit> {
  private readonly ITeamRepository _teamRepository;
  private readonly IPlayerRepository _playerRepository;

  public RemovePlayerFromTeamCommandHandler(
    ITeamRepository teamRepository,
    IPlayerRepository playerRepository
  ) {
    _teamRepository = teamRepository;
    _playerRepository = playerRepository;
  }

  public async Task<Unit> Handle(RemovePlayerFromTeamCommand command, CancellationToken token) {
    var team = await _teamRepository.FindByIdAsync(command.TeamId);
    if (team == null) {
      throw new NotFoundException("Team", command.TeamId);
    }
    
    var player = await _playerRepository.FindByIdAsync(command.PlayerId);
    if (player == null) {
      throw new NotFoundException("Player", command.PlayerId);
    }

    team.Leave(player);
    await _teamRepository.UpdateAsync(team);
    
    return Unit.Value;
  }
}