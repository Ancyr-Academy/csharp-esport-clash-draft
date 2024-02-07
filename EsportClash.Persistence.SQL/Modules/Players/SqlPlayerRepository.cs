using EsportClash.Core.Players.Model;
using EsportClash.Core.Players.Ports;

namespace EsportClash.Persistence.SQL.Modules.Players;

public class SqlPlayerRepository : SqlGenericRepository<Player>, IPlayerRepository {
  public SqlPlayerRepository(EsportDatabaseContext context): base(context) {}

  public override async Task ClearAsync() {
    _context.Players.RemoveRange(_context.Players);
    await _context.SaveChangesAsync();
  }
}