using EsportClash.Core.Shared;
using Microsoft.EntityFrameworkCore;

namespace EsportClash.Persistence.SQL;

public class SqlGenericRepository<T> : IGenericRepository<T> where T : BaseEntity {
  protected readonly EsportDatabaseContext _context;
  
  public SqlGenericRepository(EsportDatabaseContext context) {
    _context = context;
  }
  
  public async Task<T> FindByIdAsync(string id) {
    return await _context.Set<T>().FindAsync(id);
  }
  
  public async Task<List<T>> FindAllAsync() {
    return await _context.Set<T>().ToListAsync();
  }
  
  public async Task CreateAsync(T entity) {
    await _context.AddAsync(entity);
    await _context.SaveChangesAsync();
  }
  
  public async Task UpdateAsync(T entity) {
    _context.Entry(entity).State = EntityState.Modified;
    await _context.SaveChangesAsync();
  }
  
  public async Task DeleteAsync(T entity) {
    _context.Remove(entity);
    await _context.SaveChangesAsync();
  }
}