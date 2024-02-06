using EsportClash.Core.Players.Model;
using MediatR;

namespace EsportClash.Core.Teams.Commands.AddPlayerToTeam;

public class AddPlayerToTeamCommand : IRequest<Unit> {
  public String TeamId { get; set; }
  public String PlayerId { get; set; }
  public Role Role { get; set; }
}