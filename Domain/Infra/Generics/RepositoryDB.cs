using System;
using Domain.Models.Entities;

namespace Domain.Infra.Generics
{
    public class RepositoryDB<T> : IRepository<T> where T : class
    {
        public void add(T entity)
        {
            using (var db = new AcheBaratoContext())
            {
                db.Add<T>(entity);
                db.SaveChanges();
            }
        }

        public T GetElement(Func<T, bool> predicate)
        {
            using (var db = new AcheBaratoContext())
            {
                return db.Find<T>(predicate);
            }
        }
    }
}