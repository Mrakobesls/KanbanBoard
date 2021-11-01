using System.Linq;

namespace BoardApp.DAL.Repositories
{
    public interface IGenericRepository<T> where T: class
    {
        T Read(int id);
        IQueryable<T> ReadAll();
        void Delete(T entity);
        T Update(T entity);
        T Add(T entity);
    }
}
