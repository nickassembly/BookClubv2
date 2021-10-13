using AutoMapper;
using BookClub.Data;
using BookClub.Data.Entities;
using BookClub.ViewModels;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookClub.Authors
{
    [Route("api/Author")]
    [Authorize(AuthenticationSchemes =JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    public class ApiAuthorController : Controller
    {
        private readonly BookClubContext _context;
        private readonly IAuthorRepository _authorRepo;
        private readonly ILogger<ApiAuthorController> _logger;
        private readonly IMapper _mapper;

        public ApiAuthorController(BookClubContext context, IAuthorRepository authorRepo, ILogger<ApiAuthorController> logger, IMapper mapper)
        {
            _context = context;
            _authorRepo = authorRepo;
            _logger = logger;
            _mapper = mapper;
        }

        // GET: api/Authors
        [HttpGet]
        public IActionResult GetAuthors()
        {
            try
            {
                var result = _authorRepo.GetAllAuthors();
                return Ok(_mapper.Map<IEnumerable<AuthorViewModel>>(result));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to get authors: {ex}");
                return BadRequest("Failed to get authors");
            }
        }

        // GET: api/Author/5
        [HttpGet]
        public async Task<ActionResult<Author>> GetAuthor(int id)
        {
            var author = await _context.Authors.FindAsync(id);

            if (author == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<Author, AuthorViewModel>(author));
        }

        // PUT: api/Author/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAuthor(int id, Author author)
        {
            if (id != author.AuthorId)
            {
                return BadRequest();
            }

            _context.Entry(author).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AuthorExists(id))
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

        // POST: api/Author
        [HttpPost]
        public IActionResult PostAuthor([FromBody] AuthorViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    // have to assign  to new Object 
                    var newAuthor = _mapper.Map<AuthorViewModel, Author>(model);

                    _authorRepo.AddEntity(model);

                    if (_authorRepo.SaveAll())
                    {
                        return Created($"/api/Author/{newAuthor.AuthorId}", _mapper.Map<Author, AuthorViewModel>(newAuthor));
                    }
                }
                else
                {
                    return BadRequest(ModelState);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to add Author: {ex}");
            }

            return BadRequest("Failed to Save Author");
        }

        // DELETE: api/Author/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAuthor(int id)
        {
            var author = await _context.Authors.FindAsync(id);
            if (author == null)
            {
                return NotFound();
            }

            _context.Authors.Remove(author);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AuthorExists(int id)
        {
            return _context.Authors.Any(a => a.AuthorId == id);
        }

    }
}
