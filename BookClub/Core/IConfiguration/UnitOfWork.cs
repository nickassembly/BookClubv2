using BookClub.Core.IConfiguration;
using BookClub.Core.Repositories;
using BookClub.Data;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace BookClub.Core.IConfiguration
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly BookClubContext _context;
        private readonly ILogger _logger;

        public IAuthorRepository Authors { get; private set; }
        public IAuthorUserRepository AuthorUsers { get; private set; }
        public IAuthorBookRepository AuthorBooks { get; private set; }
        public IAuthorGenreRepository AuthorGenres { get; private set; }
        public IBookGenreRepository BookGenres { get; private set; }
        public IBookRepository Books { get; private set; }  
        public IGenreRepository Genres { get; private set; }

        public IUserBookRepository UserBooks { get; private set; }

        public UnitOfWork(BookClubContext context, ILoggerFactory loggerFactory)
        {
            _context = context;
            _logger = loggerFactory.CreateLogger("logs");

            Authors = new AuthorRepository(_context, _logger);
            AuthorUsers = new UserAuthorRepository(_context, _logger);
            AuthorBooks = new AuthorBookRepository(_context, _logger);
            AuthorGenres = new AuthorGenreRepository(_context, _logger);
            BookGenres = new BookGenreRepository(_context, _logger);
            Books = new BookRepository(_context, _logger);
            Genres = new GenreRepository(_context, _logger);
            UserBooks = new UserBookRepository(_context, _logger);

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
