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
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<IActionResult> UserBookDelete(int id)
        {
            await _unitOfWork.UserBooks.Delete(id);
            await _unitOfWork.CompleteAsync();
            return RedirectToAction("UserBookList");
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
        public async Task<IActionResult> AddNewBookForUser([FromForm] BookViewModel bookVM)
        {
            if (!this.User.Identity.IsAuthenticated) return RedirectToAction("Login", "Account");

            Book book = _mapper.Map<Book>(bookVM);
            try
            {

                var allBooks = _unitOfWork.Books.All();
                
                var currentUserId = UserUtils.GetLoggedInUser(this.User);
                var bookToAdd = await _unitOfWork.Books.Upsert(book);
                await _unitOfWork.CompleteAsync();

                // Grab last ID for added Book record
                var newlyAddedBook = await _unitOfWork.Books.All();

                var newBook = newlyAddedBook.Where(nab => nab.Identifier == book.Identifier && nab.IdentifierType == book.IdentifierType).FirstOrDefault();

                var newBookId = newBook.Id;
                


                await _unitOfWork.UserBooks.Upsert(new UserBook { BookId = newBookId, UserId = currentUserId });

                List<int> bookGenreIds = bookVM.GenreIds;
                List<int> authorBookIds = bookVM.AuthorIds;

                if (bookGenreIds != null)
                {
                    foreach (var genreId in bookGenreIds)
                    {
                        await _unitOfWork.BookGenres.Add(new BookGenre { BookId = newBookId, GenreId = genreId });
                    }
                }

                if (authorBookIds != null)
                {
                    foreach (var authorId in authorBookIds)
                    {
                       await  _unitOfWork.AuthorBooks.Add(new AuthorBook { BookId = newBookId, AuthorId = authorId });
                    }
                }

                await _unitOfWork.CompleteAsync();

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
