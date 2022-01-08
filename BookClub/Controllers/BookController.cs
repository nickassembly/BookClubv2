using AutoMapper;
using BookClub.Data;
using BookClub.Data.Entities;
using BookClub.ViewModels;
using BookClub.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System;
using Microsoft.AspNetCore.Mvc.Rendering;
using BookClub.Core.IConfiguration;

namespace BookClub.Controllers
{
    [Route("api/[controller]/[action]")]
        public class BookController : Controller
    {
        private readonly ILogger<BookController> _logger;
        private readonly UserManager<LoginUser> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;
        private readonly BookClubContext _context;
        private readonly IUnitOfWork _unitOfWork;

        public BookController(ILogger<BookController> logger,
            UserManager<LoginUser> userManager,
            IHttpContextAccessor httpContextAccessor,
            BookClubContext context, 
            IMapper mapper,
            IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
            _context = context;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<IActionResult> UserBookDelete(string identifier)
        {
            try
            {
                ClaimsPrincipal currentUser = this.User;
                var currentUserId = UserUtils.GetLoggedInUser(currentUser);
                var allBooks = await _unitOfWork.Books.All();
                var booksToDelete = allBooks.Where(book => book.Identifier == identifier).ToList();

                var userBooks = await _unitOfWork.UserBooks.All();

                var loggedInUserBooks = userBooks.Where(u => u.UserId == currentUserId).ToList();

                foreach(var bookToDelete in booksToDelete)
                {
                    var userBookToDelete = loggedInUserBooks.Where(userbook => userbook.UserId == currentUserId && userbook.BookId == bookToDelete.Id).FirstOrDefault();
                    if (userBookToDelete != null)
                    {
                        var deletedUserBook = _unitOfWork.UserBooks.Delete(userBookToDelete.Id);
                    }
                }

                return RedirectToAction("UserBookList");
            } 
            catch(Exception ex)
            {
                _logger.LogError($"Delete failed for user's book - Exception: {ex}");
                return RedirectToAction("UserBookList");
            }
        }


        public async Task<IActionResult> UserBookList()
        {
            if (!this.User.Identity.IsAuthenticated)
                return RedirectToAction("Login", "Account");

            try
            {
                ClaimsPrincipal currentUser = this.User;
                var currentUserId = UserUtils.GetLoggedInUser(currentUser);

                List<BookViewModel> booksToReturn = new List<BookViewModel>();

                var allBooks = await _unitOfWork.Books.All();
                var userBooks = await _unitOfWork.UserBooks.All();

                var loggedInUserBooks = userBooks.Where(u => u.UserId == currentUserId).ToList();

                foreach (var userBook in loggedInUserBooks)
                {
                    // Get all the book's authors
                    var allAuthorBooks = await _unitOfWork.AuthorBooks.All();
                    var bookAuthorIds = allAuthorBooks.Where(authorBook => authorBook.BookId == userBook.BookId)
                        .Select(authorBook => authorBook.AuthorId).ToList();

                    var allAuthors = await _unitOfWork.Authors.All();
                    List<Author> bookAuthors = allAuthors.Where(author => bookAuthorIds.Contains(author.Id)).ToList();

                    // Get all the book's genres
                    var allBookGenres = await _unitOfWork.BookGenres.All();
                    var bookGenreIds = allBookGenres.Where(bookGenre => bookGenre.BookId == userBook.BookId)
                        .Select(bookGenre => bookGenre.GenreId).ToList();

                    var allGenres = await _unitOfWork.Genres.All();
                    List<Genre> bookGenres = allGenres.Where(genre => bookGenreIds.Contains(genre.Id)).ToList();

                    BookViewModel bookVM = _mapper.Map<BookViewModel>(userBook.Book);
                    bookVM.Authors = bookAuthors;
                    bookVM.Genres = bookGenres;

                    booksToReturn.Add(bookVM);
                }

                return View(booksToReturn.ToList());
            }
            catch (Exception ex)
            {
                _logger.LogError($"List failed for Authors - Exception: {ex}");
                return StatusCode(500);
            }
        }
        private List<SelectListItem> GetGenresForSelectList()
        {
            var genres = _context.Genres.ToList();

            var genreListItem = genres.Select(genre => new SelectListItem { Text = genre.GenreName, Value = genre.Id.ToString() }).ToList();

            return genreListItem;
        }

        private List<SelectListItem> GetAuthorsForSelectList()
        {
            var authors = _context.Authors.ToList();

            var authorListItem = authors.Select(author => new SelectListItem { Text = author.Firstname + author.Lastname, Value = author.Firstname + author.Lastname }).ToList();

            return authorListItem;
        }
        
        public async Task<IActionResult> AddNewBookForUser([FromForm] BookViewModel bookVM)
        {
            if (!this.User.Identity.IsAuthenticated) return RedirectToAction("Login", "Account");

            Book book = _mapper.Map<Book>(bookVM);
            try
            {
                var existingBook = _context.Books.Where(
                                        findBook => book.Identifier == findBook.Identifier
                                        && book.IdentifierType == findBook.IdentifierType
                                        && findBook.Identifier != null).FirstOrDefault();
                if (existingBook != null)
                {
                    _logger.LogInformation($" Book already exists: {existingBook.IdentifierType} : {existingBook.Identifier} ");
                    return RedirectToAction("UserBookList");
                }

                var currentUserId = UserUtils.GetLoggedInUser(this.User);
                var bookToAdd = await _context.Books.AddAsync(book);

                await _context.SaveChangesAsync();

                _context.UserBooks.Add(new UserBook { BookId = bookToAdd.Entity.Id, UserId = currentUserId });

                List<int> bookGenreIds = bookVM.GenreIds;
                List<int> authorBookIds = bookVM.AuthorIds;

                if (bookGenreIds != null)
                {
                    foreach (var genreId in bookGenreIds)
                    {
                        _context.GenreBooks.Add(new BookGenre { BookId = bookToAdd.Entity.Id, GenreId = genreId });
                    }
                }

                if (authorBookIds != null)
                {
                    foreach (var authorId in authorBookIds)
                    {
                        _context.BookAuthors.Add(new AuthorBook { BookId = bookToAdd.Entity.Id, AuthorId = authorId });
                    }
                }

                await _context.SaveChangesAsync();

                return RedirectToAction("UserBookList");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Add failed for Book: {book} - Exception: {ex}");
                return StatusCode(500);
            }
        }
    }
}
