using MediatR;

namespace EsportClash.Core.Teams.Commands.RemovePlayerFromTeam;

public class RemovePlayerFromTeamCommand : IRequest<Unit> {
  public String TeamId { get; set; }
  public String PlayerId { get; set; }
}