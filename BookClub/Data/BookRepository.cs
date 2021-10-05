using BookClub.Data.Entities;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookClub.Data
{
    public class BookRepository : IBookRepository
    {
        private readonly BookClubContext _ctx;
        private readonly ILogger<BookRepository> _logger;

        public BookRepository(BookClubContext ctx, ILogger<BookRepository> logger )
        {
            _ctx = ctx;
            _logger = logger;
        }

        public IEnumerable<Book> GetAllBooks()
        {
            try
            {
                return _ctx.Books.ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to get all Books: {ex}");
                return null;
            }
        }

        public IEnumerable<Book> GetBooksByAuthor(Author author)
        {
            return _ctx.Books
                .Where(b => b.Authors.AuthorId == author.AuthorId)
                .ToList();
        }
        public bool SaveAll()
        {
            return _ctx.SaveChanges() > 0;
        }
    }
}
