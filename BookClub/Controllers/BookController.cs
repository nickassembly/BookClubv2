using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookClub.Controllers
{
    public class BookController : Controller
    {
        [Authorize]
        public IActionResult BookList()
        {
            return View();
        }
    }
}
