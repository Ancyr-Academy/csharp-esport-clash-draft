using MediatR;

namespace EsportClash.Core.Teams.Commands.DeleteTeam;

public class DeleteTeamCommand : IRequest<Unit> {
  public String Id { get; set; }
}