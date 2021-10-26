using BookClub.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
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
        private readonly IBookRepository _repository;

        public BookController(IBookRepository repository)
        {
            _repository = repository;
        }
        [HttpPost]
        [Route("api/book/user")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public IActionResult UserBookList()
        {
            try
            {
                var results = _repository.GetAllUserBooks();
                return Ok(results);
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpGet]
        [Route("api/book")]        
        public IActionResult BookList()
        {
            try
            {
                var results = _repository.GetAllBooks();
                return Ok(results);
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
