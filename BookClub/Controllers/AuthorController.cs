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
                foreach (var authorId in userAuthorIds)
                {
                    Author authorToAdd = await _context.Authors.Where(x => x.Id == authorId).FirstOrDefaultAsync();

                    // Get Bio from author bio table
                    AuthorBio authorBio = await _context.AuthorBios.Where(x => x.AuthorId == authorId).FirstOrDefaultAsync();

                    // Get a list of ints from Book Author Table that correspond to book ids that this author has written 
                    List<int> authorBooksIds = await _context.BookAuthors.Where(x => x.AuthorId == authorId).Select(y => y.BookId).ToListAsync();

                    List<Book> authorBooks = _context.Books.Where(b => authorBooksIds.Contains(b.Id)).ToList();

                    // Get a list of ints from Genre Author Table that correspond to genres ids that this author has written. 
                    List<int> authorGenreIds = await _context.GenreAuthors.Where(x => x.AuthorId == authorId).Select(y => y.GenreId).ToListAsync();

                    List<Genre> authorGenres = _context.Genres.Where(g => authorGenreIds.Contains(g.Id)).ToList();

                    AuthorViewModel authorVM = new AuthorViewModel
                    {
                        Firstname = authorToAdd.Firstname,
                        Lastname = authorToAdd.Lastname,
                        AuthorBio = authorBio,
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
            if (!this.User.Identity.IsAuthenticated)
                return RedirectToAction("Login", "Account");

            // TODO: Find better way to check empty properties on initial action call
            if (authorVM.Firstname == null || authorVM.Lastname == null || authorVM.AuthorBio == null) return View();

            Author author = new Author();

            try
            {
                var currentUserId = GetLoggedInUser();

                AuthorBio authorBio = new AuthorBio();
                authorBio.Nationality = authorVM.AuthorBio.Nationality;
                authorBio.BiographyDescription = authorVM.AuthorBio.BiographyDescription;
                authorBio.DateOfBirth = authorVM.AuthorBio.DateOfBirth;
                await _context.AuthorBios.AddAsync(authorBio);

                author.Firstname = authorVM.Firstname;
                author.Lastname = authorVM.Lastname;
                author.AuthorBio = authorBio;

                var authorToAdd = await _context.Authors.AddAsync(author);
                await _context.SaveChangesAsync();

                var addedAuthor = await _context.Authors.Where(a => a.Id == authorToAdd.Entity.Id).FirstOrDefaultAsync();

                // After author is added to db, need to update AuthorBooks and AuthorGenres table as well
                List<int> authorBookIds = authorVM.Books.Select(b => b.Id).ToList();
                List<int> authorGenreIds = authorVM.Genres.Select(g => g.Id).ToList();

                foreach (var bookId in authorBookIds)
                {
                    _context.BookAuthors.Add(new AuthorBook { AuthorId = addedAuthor.Id, BookId = bookId });
                }

                foreach (var genreId in authorGenreIds)
                {
                    _context.GenreAuthors.Add(new AuthorGenre { AuthorId = addedAuthor.Id, GenreId = genreId });
                }

                // Update User Author Table
                _context.UserAuthors.Add(new UserAuthor { AuthorId = addedAuthor.Id, UserId = currentUserId });

                await _context.SaveChangesAsync();

            }
            catch (Exception ex)
            {
                _logger.LogError($"Add failed for Author: {author} - Exception: {ex}");
                return StatusCode(500);
            }

            return View();
        }

        public IList<SelectListItem> GetGenres()
        {
            // CHECK SO answer https://stackoverflow.com/questions/63626110/asp-net-core-3-1-mvc-multiselect-with-list-of-objects
            // TODO: Need to find out how to handle GenreBooks and GenreAuthors
            // will need to pull these to DB when Genre list is being populated

            // TODO: Change to DB pull, using static data to test first
            var genres = new List<Genre>
            {
                new Genre { Id = 1, GenreName = "Fantasy", GenreDescription = "Dragons, magic, etc. " },
                new Genre { Id = 2, GenreName = "Science Fiction", GenreDescription = "Ships, guns, space, etc." },
                new Genre { Id = 3, GenreName = "Grim Dark", GenreDescription = "A more gritty fantasy story" },
            };

            var genreListItem = genres.Select(x => new SelectListItem { Text = x.GenreName, Value = x.Id.ToString() }).ToList();

            return genreListItem;
        }

        private string GetLoggedInUser()
        {
            ClaimsPrincipal currentUser = this.User;

            var currentUserId = currentUser.FindFirst(ClaimTypes.NameIdentifier).Value;

            return currentUserId;
        }

    }
}
