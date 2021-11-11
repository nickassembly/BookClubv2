using Ardalis.Specification.EntityFrameworkCore;
using BookClub.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookClub.Generics
{
    public class EfRepository<T> : RepositoryBase<T>, IReadRepository<T>, IRepository<T> where T : class, IAggregateRoot
    {
        public EfRepository(BookClubContext dbContext) : base(dbContext)
        {

        } 
    }
}
