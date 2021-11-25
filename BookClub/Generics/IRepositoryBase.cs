using System;
using System.Linq;
using System.Linq.Expressions;

namespace BookClub.Generics
{
    public interface IRepositoryBase<T>
    {
        IQueryable<T> List();
        IQueryable<T> ListByCondition(Expression<Func<T, bool>> expression);
        void Create(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}
