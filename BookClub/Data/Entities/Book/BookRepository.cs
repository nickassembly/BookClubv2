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
    public class BookRepository 
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
            

            var saveResult = await _ctx.SaveChangesAsync();
            return saveResult == 1;
        }
        public void AddEntity(object model)
        {
            _ctx.Add(model);
        }
    }
}
