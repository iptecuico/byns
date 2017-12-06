using System.Collections.Generic;

namespace Codetecuico.Byns.Service
{
    public interface IService<T> where T : class
    {
        IEnumerable<T> GetAll();

        T Add(T data);

        bool Update(T data);

        T GetById(int id);
    }
}
