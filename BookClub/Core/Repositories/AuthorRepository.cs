using BookClub.Core.Repositories;
using BookClub.Data;
using BookClub.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookClub.Core.Repositories
{
    public class AuthorRepository : GenericRepository<Author>, IAuthorRepository
    {
        public AuthorRepository(BookClubContext context, ILogger logger) : base(context, logger)
        {

        }

        // TODO: Add a find by Expression to avoid extra .Where queries when using
        //IQueryable<T> ListByCondition(Expression<Func<T, bool>> expression);
        // Possible implementation
        //public IQueryable<T> ListByCondition(Expression<Func<T, bool>> expression)
        //{
        //    return _bookClubContext.Set<T>().Where(expression).AsNoTracking();
        //}


        public override async Task<IEnumerable<Author>> All()
        {
            try
            {
                return await dbSet.ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} All method error", typeof(AuthorRepository));
                return new List<Author>();
            }
        }

        public override async Task<bool> Upsert(Author entity)
        {
            try
            {
                var existingAuthor = await dbSet.Where(x => x.Id == entity.Id).FirstOrDefaultAsync();

                if (existingAuthor == null)
                    return await Add(entity);

                existingAuthor.Firstname = entity.Firstname;
                existingAuthor.Lastname = entity.Lastname;
                // TODO: Rest of the properties for adding author

                return true;

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} Upsert method error", typeof(AuthorRepository));
                return false;
            }
        }

        public override async Task<bool> Delete(int id)
        {
            try
            {
                var existingEntity = await dbSet.Where(x => x.Id == id).FirstOrDefaultAsync();

                if (existingEntity != null)
                {
                    dbSet.Remove(existingEntity);
                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} Delete method error", typeof(AuthorRepository));
                return false;
            }
        }


    }
}
