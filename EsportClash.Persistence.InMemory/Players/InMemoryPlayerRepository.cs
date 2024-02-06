using EsportClash.Core.Players.Model;
using EsportClash.Core.Players.Ports;

namespace EsportClash.Persistence.InMemory.Players;

public class InMemoryPlayerRepository : GenericInMemoryRepository<Player>, IPlayerRepository {
  
}