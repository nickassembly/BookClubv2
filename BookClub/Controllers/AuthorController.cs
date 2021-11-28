using BookClub.Data;
using BookClub.Data.Entities;
using BookClub.Generics;
using BookClub.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
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
            ClaimsPrincipal currentUser = this.User;

            if (currentUser.Claims.Any())
            {
                var currentUserId = currentUser.FindFirst(ClaimTypes.NameIdentifier).Value;

                List<AuthorViewModel> authorsToReturn = new List<AuthorViewModel>();

                var userAuthorIds = _repoWrapper.UserAuthorRepo.ListByCondition(x => x.UserId == currentUserId).Select(y => y.AuthorId).ToList();

                foreach (var authorId in userAuthorIds)
                {
                    Author authorToAdd = _context.Authors.Where(x => x.Id == authorId).FirstOrDefault();

                    // Get Bio from author bio table
                    AuthorBio authorBio = _context.AuthorBios.Where(x => x.AuthorId == authorId).FirstOrDefault();

                    // Get a list of ints from Book Author Table that correspond to book ids that this author has written 
                    List<int> authorBooksIds = _context.BookAuthors.Where(x => x.AuthorId == authorId).Select(y => y.BookId).ToList();

                    // Get those corresponding titles
                    List<Book> authorBooks = _context.Books.Where(x => authorBooksIds.Contains(x.Id)).ToList();

                    // Get a list of ints from Genre Author Table that correspond to genres ids that this author has written. 
                    List<int> authorGenreIds = _context.GenreAuthors.Where(x => x.AuthorId == authorId).Select(y => y.GenreId).ToList();

                    // Get those corresponding Genres
                    List<Genre> authorGenres = _context.Genres.Where(x => authorGenreIds.Contains(x.Id)).ToList();

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

            return RedirectToAction("Login", "Account");

        }

        [HttpGet]
        public async Task<IActionResult> GetAllAuthors()
        {
           // var authors = _repoWrapper.UserAuthorRepo.List();

            // TODO:  need a wrapper around regular Author object, just User Author...

            return View();
        }

    }
}
