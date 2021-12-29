using BookClub.Core.IConfiguration;
using BookClub.Core.IRepositories;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace BookClub.Data
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly BookClubContext _context;
        private readonly ILogger _logger;

        public IAuthorRepository Authors { get; private set; }

        public UnitOfWork(BookClubContext context, ILoggerFactory loggerFactory)
        {
            _context = context;
            _logger = loggerFactory.CreateLogger("logs");

            Authors = new AuthorRepository(_context, _logger);
        }

        public async Task CompleteAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }

    }
}
