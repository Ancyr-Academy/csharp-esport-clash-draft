using EsportClash.Core.Shared;
using EsportClash.Core.Teams.Ports;
using MediatR;

namespace EsportClash.Core.Teams.Commands.DeleteTeam;

public class DeleteTeamCommandHandler : IRequestHandler<DeleteTeamCommand, Unit> { 
  private readonly ITeamRepository _teamRepository;
  
  public DeleteTeamCommandHandler(ITeamRepository teamRepository) {
    _teamRepository = teamRepository;
  }
  
  public async Task<Unit> Handle(DeleteTeamCommand request, CancellationToken cancellationToken) {
    var team = await _teamRepository.FindByIdAsync(request.Id);
    if (team == null) {
      throw new NotFoundException("Team", request.Id);
    }
    
    await _teamRepository.DeleteAsync(team);
    return Unit.Value;
  }
}