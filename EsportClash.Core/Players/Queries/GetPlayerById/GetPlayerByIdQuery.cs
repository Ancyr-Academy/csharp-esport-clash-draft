using EsportClash.Core.Players.ViewModel;
using MediatR;

namespace EsportClash.Core.Players.Queries.GetPlayerById;

public class GetPlayerByIdQuery : IRequest<PlayerViewModel> {
  public string Id { get; set; }
}