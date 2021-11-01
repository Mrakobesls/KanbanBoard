using System.Collections.Generic;

namespace BoardApp.BLL.Services
{
    public interface IGenericService<T>
    {
        T Add(T entity);
        T Read(int id);
        IList<T> ReadAll();
        void Delete(int id);
        T Update(T entity);
    }
}
