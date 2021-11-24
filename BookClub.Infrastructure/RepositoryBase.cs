using BookClub.Data;
using BookClub.Generics;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BookClub.Infrastructure
{
    public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        protected BookClubContext _bookClubContext;

        public RepositoryBase(BookClubContext bookClubContext)
        {
            _bookClubContext = bookClubContext;
        }

        public IQueryable<T> List()
        {
            return _bookClubContext.Set<T>().AsNoTracking();
        }

        public IQueryable<T> ListByCondition(Expression<Func<T, bool>> expression)
        {
            return _bookClubContext.Set<T>().Where(expression).AsNoTracking();
        }

        public void Create(T entity)
        {
            _bookClubContext.Set<T>().Add(entity);
        }

        public void Update(T entity)
        {
            _bookClubContext.Set<T>().Update(entity);
        }

        public void Delete(T entity)
        {
            _bookClubContext.Set<T>().Remove(entity);
        }
    }
}
