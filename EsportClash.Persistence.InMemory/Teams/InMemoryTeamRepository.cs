using EsportClash.Core.Teams.Ports;
using EsportClash.Core.Teams.Model;

namespace EsportClash.Persistence.InMemory.Teams;

public class InMemoryTeamRepository : GenericInMemoryRepository<Team>, ITeamRepository {
  
}