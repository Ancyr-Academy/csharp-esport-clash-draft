using EsportClash.Core.Teams.Model;
using EsportClash.Core.Teams.Ports;

namespace EsportClash.Persistence.SQL.Modules.Teams;

public class SqlTeamRepository : SqlGenericRepository<Team>, ITeamRepository {
  public SqlTeamRepository(EsportDatabaseContext context): base(context) {}
  
  public override async Task ClearAsync() {
    _context.Teams.RemoveRange(_context.Teams);
    await _context.SaveChangesAsync();
  }
}