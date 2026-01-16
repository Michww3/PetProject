using System.Linq.Expressions;

namespace PetProject.DataAccess.DbPatterns.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T> Create(T entity);
        Task Update(T entity);
        Task Delete(T entity);
        Task<T?> Get(Guid guid);
        Task<IReadOnlyList<T>> GetAll();
        Task<T?> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate);
        Task<bool> ExistsAsync(Expression<Func<T, bool>> predicate);
    }
}
