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
        public IActionResult BookList()
        {
            return View();
        }
    }
}
