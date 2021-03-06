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
    public class UserAuthorRepository : GenericRepository<UserAuthor>, IAuthorUserRepository
    {
        public UserAuthorRepository(BookClubContext context, ILogger logger) : base(context, logger)
        {

        }

        public override async Task<IEnumerable<UserAuthor>> All()
        {
            try
            {
                return await dbSet.ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} All method error", typeof(UserAuthorRepository));
                return new List<UserAuthor>();
            }
        }

        public override async Task<UserAuthor> GetById(int id)
        {
            try
            {
                return await dbSet.FindAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} All method error", typeof(UserAuthorRepository));
                return new UserAuthor();
            }
        }

        public override async Task<bool> Delete(int id)
        {
            try
            {
                var existingEntity = await dbSet.FindAsync(id);

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
