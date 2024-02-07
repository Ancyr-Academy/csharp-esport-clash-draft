using EsportClash.Core.Teams.ViewModel;
using MediatR;

namespace EsportClash.Core.Teams.Queries.GetTeamById;

public class GetTeamByIdQuery : IRequest<TeamViewModel> {
  public string Id { get; set; } = string.Empty;
}