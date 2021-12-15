using BookClub.Data;
using BookClub.Data.Entities;
using BookClub.Generics;
using BookClub.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BookClub.Controllers
{
    [Route("api/[controller]/[action]")]
    // [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class AuthorController : Controller
    {
        private readonly ILogger<AuthorController> _logger;
        private IRepositoryWrapper _repoWrapper;
        private readonly UserManager<LoginUser> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;

        private readonly BookClubContext _context;

        public AuthorController(ILogger<AuthorController> logger,
            IRepositoryWrapper repoWrapper,
            UserManager<LoginUser> userManager,
            IHttpContextAccessor httpContextAccessor, BookClubContext context)
        {
            _logger = logger;
            _repoWrapper = repoWrapper;
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
            _context = context;
        }


        [HttpGet]
        public async Task<IActionResult> UserAuthorList()
        {
            if (!this.User.Identity.IsAuthenticated)
                return RedirectToAction("Login", "Account");

            try
            {
                var currentUserId = GetLoggedInUser();

                List<AuthorViewModel> authorsToReturn = new List<AuthorViewModel>();

                var userAuthorIds = await _repoWrapper.UserAuthorRepo.ListByCondition(x => x.UserId == currentUserId).Select(y => y.AuthorId).ToListAsync();

                // TODO: Research better LINQ query to pull back child objects without so many DB trips
                // TODO: Refactor - Should be using Repo (or repowrapper) to manipulate data, not the context itself

                foreach (var authorId in userAuthorIds)
                {
                    Author authorToAdd = await _context.Authors.Where(x => x.Id == authorId).FirstOrDefaultAsync();

                    List<int> authorBooksIds = await _context.BookAuthors.Where(x => x.AuthorId == authorId).Select(y => y.BookId).ToListAsync();
                    List<Book> authorBooks = _context.Books.Where(b => authorBooksIds.Contains(b.Id)).ToList();

                    List<int> authorGenreIds = await _context.GenreAuthors.Where(x => x.AuthorId == authorId).Select(y => y.GenreId).ToListAsync();
                    List<Genre> authorGenres = _context.Genres.Where(g => authorGenreIds.Contains(g.Id)).ToList();

                    AuthorViewModel authorVM = new AuthorViewModel
                    {
                        Firstname = authorToAdd.Firstname,
                        Lastname = authorToAdd.Lastname,
                        Nationality = authorToAdd.Nationality,
                        BiographyNotes = authorToAdd.BiographyNotes,
                        Books = authorBooks,
                        Genres = authorGenres
                    };

                    authorsToReturn.Add(authorVM);
                }
                return View(authorsToReturn.ToList());

            }
            catch (Exception ex)
            {
                _logger.LogError($"List failed for Authors - Exception: {ex}");
                return StatusCode(500);
            }

        }

        public async Task<IActionResult> AddAuthor([FromForm] AuthorViewModel authorVM)
        {
            // TODO: Add validation
            if (!this.User.Identity.IsAuthenticated)
                return RedirectToAction("Login", "Account");

            // TODO: Find better way to check empty properties on initial action call
            if (authorVM.Firstname == null || authorVM.Lastname == null)
            {
                authorVM.GenreList = GetGenresForSelectList();
                authorVM.BookList = GetBooksForSelectList();

                return View(authorVM);
            }

            Author author = new Author();

            try
            {
                var currentUserId = GetLoggedInUser();

                author.Firstname = authorVM.Firstname;
                author.Lastname = authorVM.Lastname;
                author.Nationality = authorVM.Nationality;
                author.BiographyNotes = authorVM.BiographyNotes;

                var authorToAdd = await _context.Authors.AddAsync(author);
                await _context.SaveChangesAsync();

                var addedAuthor = await _context.Authors.Where(a => a.Id == authorToAdd.Entity.Id).FirstOrDefaultAsync();

                List<int> authorGenreIds = authorVM.GenreIds;
                List<int> authorBookIds = authorVM.BookIds;

                foreach (var genreId in authorGenreIds)
                {
                    _context.GenreAuthors.Add(new AuthorGenre { AuthorId = addedAuthor.Id, GenreId = genreId });
                }

                foreach (var bookId in authorBookIds)
                {
                    _context.BookAuthors.Add(new AuthorBook { AuthorId = addedAuthor.Id, BookId = bookId });
                }

                // Update User Author Table
                _context.UserAuthors.Add(new UserAuthor { AuthorId = addedAuthor.Id, UserId = currentUserId });

                await _context.SaveChangesAsync();

                return RedirectToAction("UserAuthorList");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Add failed for Author: {author} - Exception: {ex}");
                return StatusCode(500);
            }

        }

        public List<SelectListItem> GetGenresForSelectList()
        {
            var genres = _context.Genres.ToList();

            var genreListItem = genres.Select(genre => new SelectListItem { Text = genre.GenreName, Value = genre.Id.ToString() }).ToList();

            return genreListItem;
        }

        public List<SelectListItem> GetBooksForSelectList()
        {
            var books = _context.Books.ToList();

            var bookListItem = books.Select(book => new SelectListItem { Text = book.Title, Value = book.Id.ToString() }).ToList();

            return bookListItem;
        }

        private string GetLoggedInUser()
        {
            ClaimsPrincipal currentUser = this.User;

            var currentUserId = currentUser.FindFirst(ClaimTypes.NameIdentifier).Value;

            return currentUserId;
        }

    }
}
