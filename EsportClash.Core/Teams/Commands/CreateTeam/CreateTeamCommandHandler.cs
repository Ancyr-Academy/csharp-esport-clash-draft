using EsportClash.Core.Shared;
using EsportClash.Core.Shared.Id;
using EsportClash.Core.Teams.Model;
using EsportClash.Core.Teams.Ports;
using MediatR;

namespace EsportClash.Core.Teams.Commands.CreateTeam;

public class CreateTeamCommandHandler : IRequestHandler<CreateTeamCommand, IdResponse> {
  private readonly IIdProvider _idProvider;
  private readonly ITeamRepository _teamRepository;
  
  public CreateTeamCommandHandler(
    IIdProvider idProvider,
    ITeamRepository teamRepository
    ) {
    _idProvider = idProvider;
    _teamRepository = teamRepository;
  }
  
  public async Task<IdResponse> Handle(CreateTeamCommand request, CancellationToken cancellationToken) {
    var team = new Team {
      Id = _idProvider.NewId(),
      Name = request.Name
    };

    await _teamRepository.CreateAsync(team);

    return new IdResponse(team.Id);
  }
}