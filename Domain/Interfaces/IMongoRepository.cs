using System.Collections.Generic;

namespace Domain.Interfaces
{
    public interface IMongoRepository<TEntity> where TEntity : class
    {
        IEnumerable<TEntity> GetAllElements();
        void add(TEntity entity);
    }
}
