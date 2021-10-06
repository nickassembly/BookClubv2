using BookClub.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookClub.Controllers
{
    public class AccountController : Controller
    {
        private readonly ILogger<AccountController> _logger;
        public AccountController(ILogger<AccountController> logger)
        {
            logger = _logger;
        }
        public IActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (this.User.Identity.IsAuthenticated)
                {
                    return RedirectToAction("Index", "Home");
                }

            }
            else
            {
                // Show errors
            }
            return View(model);
        }
    }
}
