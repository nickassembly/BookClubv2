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
        public IActionResult UserBookList()
        {
            ClaimsPrincipal currentUser = this.User;

            if (!this.User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account");
            }

            var currentUserId = currentUser.FindFirst(ClaimTypes.NameIdentifier).Value;

            List<UserBookViewModel> userBooks = new();

            var dbuserBooks = _repoWrapper.UserBookRepo.List();
            foreach (var book in dbuserBooks)
            {

                var userBookVM = _mapper.Map<UserBookViewModel>(book);

                userBooks.Add(userBookVM); ;
            }

            return View(userBooks);
        }

        public async Task<IActionResult> AddNewBookForUser([FromForm] CreateBookViewModel bookVM)
        {
            if (!this.User.Identity.IsAuthenticated) return RedirectToAction("Login", "Account");

            ClaimsPrincipal currentUser = this.User;
            Book book = new();

            try
            {
                var currentUserId = UserUtils.GetLoggedInUser(currentUser);
                book = _mapper.Map<Book>(bookVM);

                var bookToAdd = await _context.Books.AddAsync(book); // TODO: Check if Book already exists before adding.
                await _context.SaveChangesAsync();

                var addedBook = await _context.Books.Where(b => b.Id == bookToAdd.Entity.Id).FirstOrDefaultAsync();

                IList<Author> authors = bookVM.Authors;
                
                _context.UserBooks.Add(new UserBook { BookId = addedBook.Id, UserId = currentUserId });

                if (authors != null)
                {
                    foreach (var author in authors)
                    {
                        _context.Authors.Add(new Author { Firstname = author.Firstname, Lastname = author.Lastname });
                        _context.BookAuthors.Add(new BookAuthor { AuthorId = author.Id, BookId = addedBook.Id });
                    }
                }

                await _context.SaveChangesAsync();

                return RedirectToAction("UserBookList", "Book");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Add failed for Book: {book} - Exception: {ex}");
                return StatusCode(500);
            }
        }


    }
}
