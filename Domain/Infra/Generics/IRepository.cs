using System;
using Domain.Models.Entities;

namespace Domain.Infra
{
    public interface IRepository<T> where T : Entity 
    {
        void add(T entity);
        T GetElement(Func<T,bool> predicate);
    }
}