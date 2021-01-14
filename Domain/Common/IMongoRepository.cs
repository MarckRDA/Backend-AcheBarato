using System.Collections.Generic;

namespace Domain.Common
{
    public interface IMongoRepository<TEntity> where TEntity : class
    {
        IEnumerable<TEntity> GetAllElements();
        void add(TEntity entity);
    }
}
