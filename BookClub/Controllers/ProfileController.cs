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
            // TODO: Need method to check friends and other properties of VM and return that in view
            // else return new model
            // need to recheck VM after a friend has been added to the db to get the lastest info

            return View(new LoginUserProfileViewModel());
        }

    }
}
