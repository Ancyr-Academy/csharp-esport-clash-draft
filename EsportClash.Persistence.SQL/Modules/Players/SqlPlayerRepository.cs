using EsportClash.Core.Players.Model;
using EsportClash.Core.Players.Ports;

namespace EsportClash.Persistence.SQL.Modules.Players;

public class SqlPlayerRepository : SqlGenericRepository<Player>, IPlayerRepository {
  public SqlPlayerRepository(EsportDatabaseContext context): base(context) {}

  public async Task Clear() {
    _context.Players.RemoveRange(_context.Players);
    await _context.SaveChangesAsync();
  }
}