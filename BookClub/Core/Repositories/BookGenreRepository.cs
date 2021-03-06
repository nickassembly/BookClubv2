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
    public class BookGenreRepository : GenericRepository<BookGenre>, IBookGenreRepository
    {
        public BookGenreRepository(BookClubContext context, ILogger logger) : base(context, logger)
        {

        }

        public override async Task<IEnumerable<BookGenre>> All()
        {
            try
            {
                return await dbSet.ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} All method error", typeof(BookGenreRepository));
                return new List<BookGenre>();
            }
        }



    }
}
