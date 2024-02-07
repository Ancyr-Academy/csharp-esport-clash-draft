using MediatR;

namespace EsportClash.Core.Players.Commands.DeletePlayer;

public class DeletePlayerCommand : IRequest<Unit> {
  public string Id { get; set; } = string.Empty;
}