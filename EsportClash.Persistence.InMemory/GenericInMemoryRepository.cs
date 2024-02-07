using EsportClash.Core.Shared;

namespace EsportClash.Persistence.InMemory;

public class GenericInMemoryRepository<T> : IGenericRepository<T> where T : BaseEntity {
  private Dictionary<string, T> _database = new Dictionary<string, T>();

  public async Task<T> FindByIdAsync(string id) {
    if (!_database.ContainsKey(id)) {
      return null;
    }
    
    return _database[id];
  }
  
  public async Task<List<T>> FindAllAsync() {
    return _database.Values.ToList();
  }

  public async Task CreateAsync(T entity) {
    this._database[entity.Id] = entity;
  }

  public async Task UpdateAsync(T entity) {
    this._database[entity.Id] = entity;
  }

  public async Task DeleteAsync(T entity) {
    this._database.Remove(entity.Id);
  }
  
  public void Clear() {
    _database.Clear();
  }
  
  public async Task ClearAsync() {
    Clear();
  }
}