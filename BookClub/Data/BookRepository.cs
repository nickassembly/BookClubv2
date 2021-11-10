using BookClub.Data.Entities;
using BookClub.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

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
        public bool AddNewUserbook(Book newBook)
        {
            try
            {
                var currUserId = _userManager.GetUserId(ClaimsPrincipal.Current);
                var userId = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
                var authors = new Author[]
                {
                    new Author {Firstname=newBook.Authors.FirstOrDefault().Firstname, Lastname = newBook.Authors.FirstOrDefault().Lastname}
                };
                Book book = new()
                {
                    Authors = authors,
                    Category = "New Category",
                    Title = newBook.Title,
                };
                LoginUser currentUser = _ctx.LoginUsers.Find(userId);
                var userBookCheck = _ctx.UserBooks.Where(u => u.User.Id == userId);
                if (!userBookCheck.Any())
                {
                    UserBook userBook = new()
                    {
                        User = currentUser
                    };
                    userBook.Books.Add(book);
                    _ctx.UserBooks.Add(userBook);
                }
                else
                {
                    //userBookCheck.Book.Add(book);
                }
                _ctx.Books.Add(book);
                _ctx.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to get all of User's Books: {ex}");
                return false;
            }
        }
        public UserBook GetAllUserBooks()
        {
            try
            {
                var userId = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
                var booklist = _ctx.UserBooks
                    .Where(u => u.User.Id == userId)
                    .Include(b => b.Books)
                    .ThenInclude(b => b.Authors);
                return booklist.FirstOrDefault();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to get all of User's Books: {ex}");
                return null;
            }
        }
        public IEnumerable<Book> GetAllBooks()
        {
            try
            {
                var result = _ctx.Books
                    .Include(b => b.Authors);
                return result.ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to get all Books: {ex}");
                return null;
            }
        }

        public IEnumerable<Book> GetBooksByAuthor(Author author)
        {
            return _ctx.Books.ToList(); //TODO get by author
        }
        public bool SaveAll()
        {
            return _ctx.SaveChanges() > 0;
        }
        public bool AddNewBook(Book newItem)
        {
            try
            {
                _ctx.Books.Add(newItem);
                return true;

            }
            catch (Exception ex)
            {
                _logger.LogError($"Add failed for Book: {newItem} - Exception: {ex}");
                return false;
            }

            var saveResult = _ctx.SaveChanges();
            return saveResult == 1;
        }

        public async Task<bool> AddItemAsync(GoogleBookVolume newItem)
        {
            
            _ctx.GoogleBookVolumes.Add(newItem);

            var saveResult = await _ctx.SaveChangesAsync();
            return saveResult == 1;
        }
        public void AddEntity(object model)
        {
            _ctx.Add(model);
        }
    }
}
