using MediatR;

namespace EsportClash.Core.Players.Commands.DeletePlayer;

public class DeletePlayerCommand : IRequest<Unit> {
  public String Id { get; set; }
}