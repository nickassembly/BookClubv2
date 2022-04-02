using AutoMapper;
using BookClub.Core.IConfiguration;
using BookClub.Data;
using BookClub.Data.Entities;
using BookClub.Data.Entities.User;
using BookClub.Utils;
using BookClub.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace BookClub.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly BookClubContext _context;
        private readonly IMapper _mapper;

        public HomeController(ILogger<HomeController> logger, IMapper mapper, BookClubContext context)
        {
            _logger = logger;
            _mapper = mapper;
            _context = context;
        }

        public IActionResult Index()
        {
            if (!this.User.Identity.IsAuthenticated)
                return RedirectToAction("Login", "Account");

            var userId = UserUtils.GetLoggedInUser(this.User);

            var userBookIds = _context.UserBooks.Where(x => x.UserId == userId).Select(x => x.BookId);

            var userBooks = _context.Books.Where(b => userBookIds.Contains(b.Id));

            List<BookViewModel> userBookList = new();

            foreach (var book in userBooks)
            {
                BookViewModel bookVM = _mapper.Map<BookViewModel>(book);
                userBookList.Add(bookVM);
            }

            LoginUserProfileViewModel loginProfile = new LoginUserProfileViewModel
            {
                UserBookList = userBookList,
               // Friends = userFriendsList
            };

            return View(loginProfile);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
