using EsportClash.Core.Players.Ports;
using EsportClash.Core.Shared;
using EsportClash.Core.Teams.Ports;
using EsportClash.Core.Teams.ViewModel;
using MediatR;

namespace EsportClash.Core.Teams.Queries.GetAllTeams;

public class GetAllTeamsQueryHandler : IRequestHandler<GetAllTeamsQuery, List<TeamViewModel>> {
  private readonly ITeamRepository _teamRepository;
  private readonly IPlayerRepository _playerRepository;
  
  public GetAllTeamsQueryHandler(ITeamRepository teamRepository, IPlayerRepository playerRepository) {
    _teamRepository = teamRepository;
    _playerRepository = playerRepository;
  }

  public async Task<List<TeamViewModel>> Handle(GetAllTeamsQuery query, CancellationToken token) {
    var teams = await _teamRepository.FindAllAsync();

    var viewModels = await teams
      .ToAsyncEnumerable()
      .SelectAwait(async team => {
        return new TeamViewModel {
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
    }).ToListAsync();
    
    return viewModels;
  }
}