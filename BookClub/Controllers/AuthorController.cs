using BookClub.Data;
using BookClub.Data.Entities;
using BookClub.Generics;
using BookClub.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
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

            // TODO: Add try with logging and failure result

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

                // Get those corresponding titles
                List<Book> authorBooks = await _context.Books.Where(x => authorBooksIds.Contains(x.Id)).ToListAsync();

                // Get a list of ints from Genre Author Table that correspond to genres ids that this author has written. 
                List<int> authorGenreIds = await _context.GenreAuthors.Where(x => x.AuthorId == authorId).Select(y => y.GenreId).ToListAsync();

                // Get those corresponding Genres
                List<Genre> authorGenres = await _context.Genres.Where(x => authorGenreIds.Contains(x.Id)).ToListAsync();

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

        [HttpPost]
        public async Task<IActionResult> AddAuthor(Author author)
        {
            if (!this.User.Identity.IsAuthenticated)
                return RedirectToAction("Login", "Account");
            
            try
            {
                var currentUserId = GetLoggedInUser();

                // TODO: Add other tables related to author entity



                await _context.Authors.AddAsync(author);
                await _context.SaveChangesAsync();

            }
            catch (Exception ex)
            {
                _logger.LogError($"Add failed for Author: {author} - Exception: {ex}");
                // Add failure result 500?
            }


            return View();
        }

        private string GetLoggedInUser()
        {
            ClaimsPrincipal currentUser = this.User;

            var currentUserId = currentUser.FindFirst(ClaimTypes.NameIdentifier).Value;

            return currentUserId;
        }

    }
}
