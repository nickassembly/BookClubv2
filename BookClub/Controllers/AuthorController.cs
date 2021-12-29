using AutoMapper;
using BookClub.Core.IConfiguration;
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
       // private readonly ILogger<AuthorController> _logger;
        private IRepositoryWrapper _repoWrapper;
        private readonly UserManager<LoginUser> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;

        private readonly ILogger<AuthorController> _logger;
        private readonly IUnitOfWork _unitOfWork;

        private readonly BookClubContext _context;

        public AuthorController(/*ILogger<AuthorController> logger,*/
            ILogger<AuthorController> logger,
            IUnitOfWork unitOfWork,
            IRepositoryWrapper repoWrapper,
            UserManager<LoginUser> userManager,
            IHttpContextAccessor httpContextAccessor, BookClubContext context, IMapper mapper)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _repoWrapper = repoWrapper;
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
            _context = context;
            _mapper = mapper;
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

                var userAuthorIds = await _repoWrapper.AuthorUserRepo.ListByCondition(user => user.UserId == currentUserId).Select(y => y.AuthorId).ToListAsync();

                var allAuthors = await _unitOfWork.Authors.All();
                // TODO: Add Repo for user author and get them all here...

                foreach (var authorId in userAuthorIds)
                {

                    var authorToAdd = await _repoWrapper.AuthorUserRepo
                        .ListByCondition(userAuthor => userAuthor.AuthorId == authorId)
                        .Select(userAuthor => userAuthor.Author).FirstOrDefaultAsync();

                    var authorBooksIds = await _repoWrapper.AuthorBookRepo
                        .ListByCondition(authorBook => authorBook.AuthorId == authorId)
                        .Select(authorBook => authorBook.BookId).ToListAsync();

                    // TODO: Repo object for books and genres, refactor add book to use repo
                    List<Book> authorBooks = _context.Books.Where(b => authorBooksIds.Contains(b.Id)).ToList();

                    var authorGenreIds = await _repoWrapper.AuthorGenreRepo
                        .ListByCondition(authorGenre => authorGenre.AuthorId == authorId)
                        .Select(authorGenre => authorGenre.GenreId).ToListAsync();

                    List<Genre> authorGenres = _context.Genres.Where(g => authorGenreIds.Contains(g.Id)).ToList();

                    AuthorViewModel authorVM = _mapper.Map<AuthorViewModel>(authorToAdd);
                    authorVM.Books = authorBooks;
                    authorVM.Genres = authorGenres;

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

            if (!ModelState.IsValid)
            {
                authorVM.GenreList = GetGenresForSelectList();
                authorVM.BookList = GetBooksForSelectList();

                return View("/Views/Author/AddAuthor.cshtml", authorVM);
            }

            Author author = _mapper.Map<Author>(authorVM);

            try
            {
                var currentUserId = GetLoggedInUser();

                // TODO: Modify Create method to return object created and avoid having to order the list below
                _repoWrapper.AuthorRepo.Create(author); 
                _repoWrapper.Save();
                var authorToAdd = _repoWrapper.AuthorRepo.List().OrderByDescending(x => x.Id).First();

                var addedAuthor = await _repoWrapper.AuthorRepo.ListByCondition(author => author.Id == authorToAdd.Id).FirstOrDefaultAsync();

                List<int> authorGenreIds = authorVM.GenreIds;
                List<int> authorBookIds = authorVM.BookIds;

                if (authorGenreIds != null)
                {
                    foreach (var genreId in authorGenreIds)
                    {
                        _repoWrapper.AuthorGenreRepo.Create(new AuthorGenre { AuthorId = addedAuthor.Id, GenreId = genreId });
                    }
                }

                if (authorBookIds != null)
                {
                    foreach (var bookId in authorBookIds)
                    {
                        _repoWrapper.AuthorBookRepo.Create(new AuthorBook { AuthorId = addedAuthor.Id, BookId = bookId });
                    }
                }

                _repoWrapper.AuthorUserRepo.Create(new UserAuthor { AuthorId = addedAuthor.Id, UserId = currentUserId });
                _repoWrapper.Save();

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
