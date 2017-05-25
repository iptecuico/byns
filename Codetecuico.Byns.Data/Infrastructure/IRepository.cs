using System.Collections.Generic;

namespace Codetecuico.Byns.Data.Infrastructure
{
    public interface IRepository<T> where T : class
    {
        // Marks an entity as new
        T Add(T entity);

        // Marks an entity as modified
        void Update(T entity);

        // Marks an entity to be removed
        void Delete(T entity);

        // Get an entity by int id
        T GetById(int id);

        // Gets all entities of type T
        IEnumerable<T> GetAll();
    }
}