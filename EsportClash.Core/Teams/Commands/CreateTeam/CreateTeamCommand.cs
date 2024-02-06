using EsportClash.Core.Shared;
using MediatR;

namespace EsportClash.Core.Teams.Commands.CreateTeam;

public class CreateTeamCommand : IRequest<IdResponse> {
  public string Name { get; set; } = string.Empty;
}