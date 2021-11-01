using System.Linq;

namespace BoardApp.DAL.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> 
        where T : class
    {
        protected readonly BoardDbContext Context;

        public GenericRepository(BoardDbContext context)
        {
            Context = context;
        }

        public T Add(T entity)
        {
            var result =  Context.Add(entity).Entity;

            return result;
        }

        public void Delete(T entity)
        {
            Context.Remove(entity);
        }

        public T Read(int id)
        {
            return Context.Find<T>(id);
        }

        public IQueryable<T> ReadAll()
        {
            return Context.Set<T>();
        }

        public T Update(T entity)
        {
            var result =  Context.Update(entity).Entity;

            return result;
        }
    }
}
