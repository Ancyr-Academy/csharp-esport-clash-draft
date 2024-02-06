using EsportClash.Core.Players.Ports;
using EsportClash.Core.Shared;
using EsportClash.Core.Teams.Ports;
using MediatR;

namespace EsportClash.Core.Teams.Commands.AddPlayerToTeam;

public class AddPlayerToTeamCommandHandler : IRequestHandler<AddPlayerToTeamCommand, Unit> {
  private readonly ITeamRepository _teamRepository;
  private readonly IPlayerRepository _playerRepository;

  public AddPlayerToTeamCommandHandler(
    ITeamRepository teamRepository,
    IPlayerRepository playerRepository
  ) {
    _teamRepository = teamRepository;
    _playerRepository = playerRepository;
  }

  public async Task<Unit> Handle(AddPlayerToTeamCommand command, CancellationToken token) {
    var team = await _teamRepository.FindByIdAsync(command.TeamId);
    if (team == null) {
      throw new NotFoundException("Team", command.TeamId);
    }
    
    var player = await _playerRepository.FindByIdAsync(command.PlayerId);
    if (player == null) {
      throw new NotFoundException("Player", command.PlayerId);
    }

    team.Join(player, command.Role);
    await _teamRepository.UpdateAsync(team);
    
    return Unit.Value;
  }
}