using AutoMapper;
using BookClub.Core.IConfiguration;
using BookClub.Data;
using BookClub.Data.Entities;
using BookClub.Utils;
using BookClub.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.DependencyInjection;
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

    // TODO: Revise Author functionality -- 
    // in the same way that books are displayed, Reference AddUserBookPartial
    // should only need -- 1 place to Add authors or books
    public class AuthorController : Controller
    {
        private readonly IMapper _mapper;
        private readonly ILogger<AuthorController> _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly BookClubContext _context;
        private readonly IEmailService _emailService;

        [ActivatorUtilitiesConstructor]
        public AuthorController(
            IMapper mapper,
            ILogger<AuthorController> logger,
            IUnitOfWork unitOfWork,
            BookClubContext context,
            IEmailService emailService)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _context = context;
            _mapper = mapper;
            _emailService = emailService;
        }

        // TODO: Find a better way around multiple constructors for testings
        public AuthorController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
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

                var allAuthors = await _unitOfWork.Authors.All();
                var userAuthors = await _unitOfWork.AuthorUsers.All();

                var loggedInUserAuthors = userAuthors.Where(u => u.UserId == currentUserId).ToList();

                foreach (var userAuthor in loggedInUserAuthors)
                {
                    // Get all the authors books
                    var allAuthorBooks = await _unitOfWork.AuthorBooks.All();
                    var authorBookIds = allAuthorBooks.Where(authorBook => authorBook.AuthorId == userAuthor.AuthorId)
                        .Select(authorBook => authorBook.BookId).ToList();

                    var allBooks = await _unitOfWork.Books.All();
                    List<Book> authorBooks = allBooks.Where(book => authorBookIds.Contains(book.Id)).ToList();

                    // Get all the authors genres
                    var allAuthorGenres = await _unitOfWork.AuthorGenres.All();
                    var authorGenreIds = allAuthorGenres.Where(authorGenre => authorGenre.AuthorId == userAuthor.AuthorId)
                        .Select(authorGenre => authorGenre.GenreId).ToList();

                    var allGenres = await _unitOfWork.Genres.All();
                    List<Genre> authorGenres = allGenres.Where(genre => authorGenreIds.Contains(genre.Id)).ToList();

                    // TODO: Issues with Test when using mapper (null reference). Refactor to make automapper work with tests
                    // AuthorViewModel authorVM = _mapper.Map<AuthorViewModel>(userAuthor.Author);
                    //authorVM.Books = authorBooks;
                    //authorVM.Genres = authorGenres;

                    AuthorViewModel authorVM = new AuthorViewModel
                    {
                        Id = userAuthor.Id,
                        Firstname = userAuthor.Author.Firstname,
                        Lastname = userAuthor.Author.Lastname,
                        BiographyNotes = userAuthor.Author.BiographyNotes,
                        Nationality = userAuthor.Author.Nationality,
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

        [HttpGet]
        public async Task<IActionResult> GetUserAuthorById(int id)
        {
            var userAuthor = await _unitOfWork.AuthorUsers.GetById(id);
            await _unitOfWork.CompleteAsync();

            if (userAuthor == null) return NotFound();

            return Ok(userAuthor);
        }

        public async Task<IActionResult> AddAuthor([FromForm] AuthorViewModel authorVM)
        {
            if (!this.User.Identity.IsAuthenticated)
                return RedirectToAction("Login", "Account");

            if (!ModelState.IsValid)
            {
                authorVM.GenreList = await GetGenresForSelectList();
                authorVM.BookList = await GetBooksForSelectList();

                return View("/Views/Author/AddAuthor.cshtml", authorVM);
            }

            //Author author = _mapper.Map<Author>(authorVM);
            Author author = new Author
            {
                Id = authorVM.Id,
                Firstname = authorVM.Firstname,
                Lastname = authorVM.Lastname,
                Nationality = authorVM.Nationality,
                BiographyNotes = authorVM.BiographyNotes,
            };

            try
            {
                var currentUserId = GetLoggedInUser();

                await _unitOfWork.Authors.Add(author);
                await _unitOfWork.CompleteAsync();

                List<int> authorGenreIds = authorVM.GenreIds;
                List<int> authorBookIds = authorVM.BookIds;

                if (authorGenreIds != null)
                {
                    foreach (var genreId in authorGenreIds)
                    {
                        await _unitOfWork.AuthorGenres.Add(new AuthorGenre { AuthorId = author.Id, GenreId = genreId });
                    }
                }

                if (authorBookIds != null)
                {
                    foreach (var bookId in authorBookIds)
                    {
                        await _unitOfWork.AuthorBooks.Add(new AuthorBook { AuthorId = author.Id, BookId = bookId });
                    }
                }

                await _unitOfWork.AuthorUsers.Add(new UserAuthor { AuthorId = author.Id, UserId = currentUserId });
                await _unitOfWork.CompleteAsync();

                return RedirectToAction("UserAuthorList");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Add failed for Author: {author} - Exception: {ex}");
                return StatusCode(500);
            }
        }

        public async Task<IActionResult> DeleteAuthor(int id)
        {
            await _unitOfWork.AuthorUsers.Delete(id);
            await _unitOfWork.CompleteAsync();

            return RedirectToAction("UserAuthorList");
        }

        public async Task<List<SelectListItem>> GetGenresForSelectList()
        {
            var genres = await _unitOfWork.Genres.All();
            await _unitOfWork.CompleteAsync();

            var genreListItem = genres.Select(genre => new SelectListItem { Text = genre.GenreName, Value = genre.Id.ToString() }).ToList();

            return genreListItem;
        }

        public async Task<List<SelectListItem>> GetBooksForSelectList()
        {
            var books = await _unitOfWork.Books.All();
            await _unitOfWork.CompleteAsync();

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
