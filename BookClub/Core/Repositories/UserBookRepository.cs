using BookClub.Data;
using BookClub.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BookClub.Core.Repositories
{
    public class UserBookRepository : GenericRepository<UserBook>, IUserBookRepository
    {
        public UserBookRepository(BookClubContext context, ILogger logger) : base(context, logger)
        {

        }

        public override async Task<IEnumerable<UserBook>> All()
        {
            try
            {
                return await dbSet.ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} All method error", typeof(UserBookRepository));
                return new List<UserBook>();
            }
        }


        public IQueryable ListByCondition(Expression<Func<UserBook, bool>> expression)
        {
            return dbSet.Where(expression);
        }

        public int ReturnLast()
        {
            var lastUserBook = dbSet.Last();
            return lastUserBook.Id;
        }

        public override async Task<bool> Upsert(UserBook entity)
        {
            try
            {
                var existingUserBook = await dbSet.Where(x => x.Id == entity.Id).FirstOrDefaultAsync();

                if (existingUserBook == null)
                    return await Add(entity);

                existingUserBook.BookId = entity.BookId;
                existingUserBook.UserId = entity.UserId;

                return true;

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} Upsert method error", typeof(UserBookRepository));
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
                _logger.LogError(ex, "{Repo} Delete method error", typeof(UserBookRepository));
                return false;
            }
        }
    }
}
