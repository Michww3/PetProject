using System.Linq.Expressions;

namespace PetProject.DataAccess.DbPatterns.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T> CreateAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);
        Task<T?> GetAsync(Guid guid);
        Task<IReadOnlyList<T>> GetAllAsync();
        Task<T?> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate);
        Task<bool> ExistsAsync(Expression<Func<T, bool>> predicate);
        Task<IReadOnlyList<T>> GetListAsync(Expression<Func<T, bool>> predicate);
    }
}
