using System;
using Domain.Models.Entities;

namespace Domain.Infra
{
    public interface IRepository<T> where T : class 
    {
        void add(T entity);
        T GetElement(Func<T,bool> predicate);
    }
}