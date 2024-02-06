namespace EsportClash.Core.Shared;

public interface IGenericRepository<T> where T : BaseEntity {
  Task<T?> FindByIdAsync(string id);
  Task<List<T>> FindAllAsync();
  Task CreateAsync(T entity);
  Task UpdateAsync(T entity);
  Task DeleteAsync(T entity);
}