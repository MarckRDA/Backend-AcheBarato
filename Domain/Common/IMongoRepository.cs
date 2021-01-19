using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Domain.Common
{
    public interface IMongoRepository<TEntity> where TEntity : class
    {
        IEnumerable<TEntity> GetAllElements();
        TEntity GetEntityById(Expression<Func<TEntity, Guid>> function, Guid value);
        void add(TEntity entity);
    }
}
