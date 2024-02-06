using EsportClash.Core.Players.Model;
using EsportClash.Core.Shared;
using MediatR;

namespace EsportClash.Core.Players.Commands.CreatePlayer;

public class CreatePlayerCommand : IRequest<IdResponse> {
  public string Name { get; set; } = string.Empty;
  public Role MainRole { get; set; }
}