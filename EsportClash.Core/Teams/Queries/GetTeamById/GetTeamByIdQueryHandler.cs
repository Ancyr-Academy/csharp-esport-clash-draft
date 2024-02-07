using EsportClash.Core.Players.Ports;
using EsportClash.Core.Shared;
using EsportClash.Core.Teams.Ports;
using EsportClash.Core.Teams.ViewModel;
using MediatR;

namespace EsportClash.Core.Teams.Queries.GetTeamById;

public class GetTeamByIdQueryHandler : IRequestHandler<GetTeamByIdQuery, TeamViewModel> {
  private readonly ITeamRepository _teamRepository;
  private readonly IPlayerRepository _playerRepository;
  
  public GetTeamByIdQueryHandler(ITeamRepository teamRepository, IPlayerRepository playerRepository) {
    _teamRepository = teamRepository;
    _playerRepository = playerRepository;
  }

  public async Task<TeamViewModel> Handle(GetTeamByIdQuery query, CancellationToken token) {
    var team = await _teamRepository.FindByIdAsync(query.Id);
    if (team == null) {
      throw new NotFoundException("Team", query.Id);
    }

    var viewModel = new TeamViewModel {
      Id = team.Id,
      Name = team.Name,
      Members = await team.Members
        .ToAsyncEnumerable()
        .SelectAwait(async member => {
          var player = await _playerRepository.FindByIdAsync(member.PlayerId);
          if (player == null) {
            return null;
          }
          
          return new TeamViewModel.Member {
            PlayerId = player.Id,
            Name = player.Name,
            Role = member.Role.ToString()
          };
        })
        .Where(member => member != null)
        .ToListAsync() as List<TeamViewModel.Member>
    };
    
    return viewModel;
  }
}