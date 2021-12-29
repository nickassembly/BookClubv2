using AutoMapper;
using BookClub.Data;
using BookClub.Data.Entities;
using BookClub.Generics;
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

namespace BookClub.Controllers
{
    [Route("api/[controller]/[action]")]
    // [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class BookController : Controller
    {
        private readonly ILogger<BookController> _logger;
        private IRepositoryWrapper _repoWrapper;
        private readonly UserManager<LoginUser> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;
        private readonly BookClubContext _context;

        public BookController(ILogger<BookController> logger,
            IRepositoryWrapper repoWrapper,
            UserManager<LoginUser> userManager,
            IHttpContextAccessor httpContextAccessor,
            BookClubContext context, 
            IMapper mapper)
        {
            _logger = logger;
            _repoWrapper = repoWrapper;
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult UserBookDelete(BookViewModel book)
        {
            try
            {
                ClaimsPrincipal currentUser = this.User;
                var currentUserId = UserUtils.GetLoggedInUser(currentUser);
                var bookToDelete = _context.Books.Where(bk => bk.IdentifierType == book.IdentifierType && bk.Identifier == book.Identifier).FirstOrDefault();
                var userBook = _context.UserBooks.Where(u => u.BookId == bookToDelete.Id && u.UserId == currentUserId).FirstOrDefault();

                if (userBook != null)
                {
                    _context.UserBooks.Remove(userBook);
                    _context.SaveChanges();
                    return RedirectToAction("UserBookList");
                }
                return RedirectToAction("UserBookList");
            } 
            catch(Exception ex)
            {
                _logger.LogError($"Delete failed for user's book - Exception: {ex}");
                return StatusCode(500);
            }
        }

        [HttpGet]
        public async Task<IActionResult> UserBookList()
        {
            if (!this.User.Identity.IsAuthenticated)
                return RedirectToAction("Login", "Account");

            try
            {
                ClaimsPrincipal currentUser = this.User;
                var currentUserId = UserUtils.GetLoggedInUser(currentUser);

                List<BookViewModel> booksToReturn = new();

                var userBookIds = await _repoWrapper.UserBookRepo.ListByCondition(user => user.UserId == currentUserId).Select(y => y.BookId).ToListAsync();

                foreach (var bookId in userBookIds)
                {

                    var bookToReturn = await _repoWrapper.UserBookRepo
                        .ListByCondition(userBook => userBook.BookId == bookId)
                        .Select(userBook => userBook.Book).FirstOrDefaultAsync();


                    // List<int> authorBooksIds = await _context.BookAuthors.Where(x => x.AuthorId == authorId).Select(y => y.BookId).ToListAsync();

                    var bookAuthorIds = await _repoWrapper.UserBookRepo
                        .ListByCondition(userBook => userBook.BookId == bookId)
                        .Select(authorBook => authorBook.BookId).ToListAsync();

                    List<Book> plainBooks = _context.Books.Where(b => bookAuthorIds.Contains(b.Id)).ToList();
                    List<Author> authorBooks = _context.Authors.Where(author => bookAuthorIds.Contains(author.Id)).ToList();

                    // List<int> authorGenreIds = await _context.GenreAuthors.Where(x => x.AuthorId == authorId).Select(y => y.GenreId).ToListAsync();

                    var bookGenreIds = await _repoWrapper.BookGenreRepo
                        .ListByCondition(bookGenre => bookGenre.BookId == bookId)
                        .Select(authorGenre => authorGenre.GenreId).ToListAsync();

                    // TODO: Add Repo Wrapper for Genres
                    List<Genre> bookGenres = _context.Genres.Where(genre => bookGenreIds.Contains(genre.Id)).ToList();

                   var bookVM = _mapper.Map<BookViewModel>(bookToReturn);
                    bookVM.Authors = authorBooks;
                    bookVM.Genres = bookGenres;

                    booksToReturn.Add(bookVM);
                }
                return View(booksToReturn);

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

        public async Task<IActionResult> AddNewBookForUser([FromBody] BookViewModel bookVM)
        {
            if (!this.User.Identity.IsAuthenticated) return RedirectToAction("Login", "Account");
            if (!this.ModelState.Any())
            {
                bookVM.GenreList = GetGenresForSelectList();
                bookVM.AuthorList = GetAuthorsForSelectList();

                return View(bookVM);
            }

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

                var addedBook = await _repoWrapper.BookRepo.ListByCondition(book => book.Id == bookToAdd.Entity.Id).FirstOrDefaultAsync();

                _context.UserBooks.Add(new UserBook { BookId = addedBook.Id, UserId = currentUserId });


                List<int> bookGenreIds = bookVM.GenreIds;
                List<int> authorBookIds = bookVM.AuthorIds;

                if (bookGenreIds != null)
                {
                    foreach (var genreId in bookGenreIds)
                    {
                        _context.GenreBooks.Add(new BookGenre { BookId = addedBook.Id, GenreId = genreId });
                    }
                }

                if (authorBookIds != null)
                {
                    foreach (var authorId in authorBookIds)
                    {
                        _context.BookAuthors.Add(new BookAuthor { BookId = addedBook.Id, AuthorId = authorId });
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
