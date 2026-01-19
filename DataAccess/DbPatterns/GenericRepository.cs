using Microsoft.EntityFrameworkCore;
using PetProject.Data;
using PetProject.DataAccess.DbPatterns.Interfaces;
using System.Linq.Expressions;

namespace PetProject.DataAccess.DbPatterns
{
    //split a Tracked/NoTracked method
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly AppDbContext _context;
        private readonly DbSet<T> _dbSet;

        public GenericRepository(AppDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public async Task<T> CreateAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
            return entity;
        }

        public async Task DeleteAsync(T entity)
        {
            _dbSet.Remove(entity);
            await Task.CompletedTask;
        }

        public async Task<T?> GetAsync(Guid id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task<IReadOnlyList<T>> GetAllAsync()
        {
            return await _dbSet.AsNoTracking().ToListAsync();
        }

        public async Task UpdateAsync(T entity)
        {
            _dbSet.Update(entity);
            await Task.CompletedTask;
        }

        public async Task<T?> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate)
        {
            return await _dbSet.AsNoTracking().FirstOrDefaultAsync(predicate);
        }

        public async Task<bool> ExistsAsync(Expression<Func<T, bool>> predicate)
        {
            return await _dbSet.AnyAsync(predicate);
        }

        public async Task<IReadOnlyList<T>> GetListAsync(Expression<Func<T, bool>> predicate)
        {
            return await _dbSet.AsNoTracking().Where(predicate).ToListAsync();
        }
    }
}
