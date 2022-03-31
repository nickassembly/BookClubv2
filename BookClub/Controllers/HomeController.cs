using BookClub.Data.Entities.User;
using BookClub.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Diagnostics;

namespace BookClub.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            // TODO: Need to populate a LoginUser Profile and send

            if (!this.User.Identity.IsAuthenticated)
                return RedirectToAction("Login", "Account");

            LoginUserProfileViewModel loginProfile = new LoginUserProfileViewModel
            {
                UserBookList = new List<BookViewModel>(), // Get users booklist here
                Friends = new List<LoginUserFriendship>() // Get user friends here
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
