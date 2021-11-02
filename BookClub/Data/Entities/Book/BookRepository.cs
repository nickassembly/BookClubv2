using BookClub.Data.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BookClub.Data
{
    public class BookRepository : IBookRepository
    {
        private readonly BookClubContext _ctx;
        private readonly ILogger<BookRepository> _logger;
        private readonly UserManager<LoginUser> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public BookRepository(BookClubContext ctx, ILogger<BookRepository> logger, UserManager<LoginUser> userManager, IHttpContextAccessor httpContextAccessor)
        {
            _ctx = ctx;
            _logger = logger;
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;

        }
        public UserBook GetAllUserBooks()
        {
            throw new NotImplementedException();
            //try
            //{
            //    string userId = _httpContextAccessor.HttpContext.User.FindFirstValue("Id");
            //    var booklist = _ctx.UserBooks
            //        .Where(u => u.User.Id == userId)
            //        .Include(b => b.Books)
            //        .ThenInclude(a => a.Authors)
            //        .FirstOrDefault();
            //    return booklist;
            //}
            //catch (Exception ex)
            //{
            //    _logger.LogError($"Failed to get all of User's Books: {ex}");
            //    return null;
            //}
        }
        public IEnumerable<Book> GetAllBooks()
        {
            throw new NotImplementedException();
            //try
            //{
            //    var result = _ctx.Books
            //        .Include(b => b.Authors);
            //    return result.ToList();
            //}
            //catch (Exception ex)
            //{
            //    _logger.LogError($"Failed to get all Books: {ex}");
            //    return null;
            //}
        }

        public IEnumerable<Book> GetBooksByAuthor(Author author)
        {
            return _ctx.Books.ToList(); //TODO get by author
        }
        public bool SaveAll()
        {
            return _ctx.SaveChanges() > 0;
        }

        public void AddEntity(object model)
        {
            _ctx.Add(model);
        }
    }
}
