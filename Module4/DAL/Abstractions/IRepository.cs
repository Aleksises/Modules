using System.Collections.Generic;

namespace DAL.Abstractions
{
    public interface IRepository<T>
    {
        T Add(T item);

        IEnumerable<T> GetAll();

        T Get(int id);

        void Delete(int id);
    }
}
