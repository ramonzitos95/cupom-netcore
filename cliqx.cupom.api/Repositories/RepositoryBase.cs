
using cliqx.cupom.api.Repositories.Contracts;

namespace cliqx.cupom.api
{
    public class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        
        public DataContext Context { get; }
        
        public RepositoryBase(DataContext _context)
        {
            Context = _context;
        }

        public void Add<T>(T entity) where T : class
        {
            Context.AddAsync(entity);
        }

        public void AddRange<T>(IEnumerable<T> entities) where T : class
        {
            Context.AddRangeAsync(entities);
        }

        public void Delete<T>(T entity) where T : class
        {
            Context.Remove(entity);
        }

        public void DeleteRange<T>(IEnumerable<T> entities) where T : class
        {
            Context.RemoveRange(entities);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await Context.SaveChangesAsync()) > 0;
        }

        public void Update<T>(T entity) where T : class
        {
            Context.Update(entity);
        }

        public void UpdateRange<T>(IEnumerable<T> entities) where T : class
        {
            Context.UpdateRange(entities);
        }
    }
}