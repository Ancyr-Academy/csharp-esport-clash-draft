using EsportClash.Core.Players.ViewModel;
using MediatR;

namespace EsportClash.Core.Players.Queries.GetAllPlayers;

public class GetAllPlayersQuery : IRequest<List<PlayerViewModel>> {
  
}