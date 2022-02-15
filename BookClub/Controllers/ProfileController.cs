using BookClub.Core.IConfiguration;
using BookClub.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookClub.Controllers
{
    [Route("api/[controller]/[action]")]
    public class ProfileController : Controller
    {
        private readonly ILogger<ProfileController> _logger;
        private readonly IUnitOfWork _unitOfWork;

        public ProfileController(ILogger<ProfileController> logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {

            return View(new LoginUserProfileViewModel());
        }

        public IActionResult AddFriend(int userId)
        {
            return RedirectToAction("Index");
        }

        // TODO: Add Action to Display new View which shows Current Friends
        // View needs buttons to add/edit current friends 
    }
}
