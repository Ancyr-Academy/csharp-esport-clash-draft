using EsportClash.Core.Teams.ViewModel;
using MediatR;

namespace EsportClash.Core.Teams.Queries.GetAllTeams;

public class GetAllTeamsQuery : IRequest<List<TeamViewModel>> {
}