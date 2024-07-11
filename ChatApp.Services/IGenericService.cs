using ChatApp.Data;

namespace ChatApp.Services;

public interface IGenericService<T> where T:BaseEntitiy
{
    Task<T> GetByIdAsync(int id);
    Task<IReadOnlyList<T>> ListAllAsync(PaginationModel paginationModel);
    Task<T> AddAsync(T entity);
    Task<T> UpdateAsync(T entity);
    Task<T> DeleteAsync(T entity);
    Task<int> CountAsync();

}
