using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BookClub.Data;
using BookClub.Data.Entities;
using Microsoft.Extensions.Logging;
using BookClub.ViewModels;
using BookClub.Controllers;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace BookClub.Controllers
{
    [Route("api/Book")]
    [Authorize(AuthenticationSchemes=JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    public class ApiBookController : ControllerBase
    {
        private readonly BookClubContext _context;
        private readonly IBookRepository _repository;
        private readonly ILogger<ApiBookController> _logger;
        private readonly IMapper _mapper;

        public ApiBookController(ILogger<ApiBookController> logger,
            IBookRepository repository,
            BookClubContext context,
            IMapper mapper)
        {
            _context = context;
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }

        // GET: api/Book
        [HttpGet]
        public IActionResult GetBooks()
        {
            try
            {
                var result = _repository.GetAllBooks();
                return Ok(_mapper.Map<IEnumerable<BookViewModel>>(result));

            }
            catch(Exception ex)
            {
                _logger.LogError($"Failed to get books: {ex}");
                return BadRequest("Failed to get books.");
            }
        }

        // GET: api/Book/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Book>> GetBook(int id)
        {
            var book = await _context.Books.FindAsync(id);

            if (book == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<Book, BookViewModel>(book));
        }

        // PUT: api/Book/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBook(int id, Book book)
        {
            if (id != book.BookId)
            {
                return BadRequest();
            }

            _context.Entry(book).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BookExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Book
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public IActionResult PostBook([FromBody]BookViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    // have to assign  to new Object 
                    var newBook = _mapper.Map<BookViewModel, Book>(model);

                    if (newBook.PublishDate == DateTime.MinValue)
                    {
                        newBook.PublishDate = DateTime.Now;
                    }
                    _repository.AddEntity(model);
                    if (_repository.SaveAll())
                    {
                        return Created($"/api/Book/{newBook.BookId}", _mapper.Map<Book, BookViewModel>(newBook));
                    }
                }
                else
                {
                    return BadRequest(ModelState);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to add Book: {ex}");
            }

            return BadRequest("Failed to Save Book");
        }

        // DELETE: api/Book/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            var book = await _context.Books.FindAsync(id);
            if (book == null)
            {
                return NotFound();
            }

            _context.Books.Remove(book);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BookExists(int id)
        {
            return _context.Books.Any(e => e.BookId == id);
        }
    }
}
