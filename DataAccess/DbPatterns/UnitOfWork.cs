using PetProject.Data;
using PetProject.DataAccess.DbPatterns.Interfaces;

namespace PetProject.DataAccess.DbPatterns
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;
        private bool _disposed = false;
        private readonly Dictionary<Type, object> _repositories = new();

        public UnitOfWork(AppDbContext context)
        {
            _context = context;
        }

        public IGenericRepository<T> Repository<T>() where T : class
        {
            var type = typeof(T);
            if (!_repositories.ContainsKey(type))
            {
                _repositories[type] = new GenericRepository<T>(_context);
            }
            return (IGenericRepository<T>)_repositories[type];
        }

        public Task<int> SaveChangesAsync()
            => _context.SaveChangesAsync();

        public async ValueTask DisposeAsync()
        {
            if (!_disposed)
            {
                await _context.DisposeAsync();
                _repositories.Clear();
                _disposed = true;
            }
        }
    }
}
