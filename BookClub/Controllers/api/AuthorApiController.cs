using AutoMapper;
using AutoMapper.Configuration;
using BookClub.Authors;
using BookClub.Data;
using BookClub.Data.Entities;
using BookClub.ViewModels;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookClub.Controllers.api
{
    [Route("api/Author")]
   // [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    public class AuthorApiController : Controller
    {
        private readonly BookClubContext _context;
        private readonly IAuthorRepository _repository;
        private readonly ILogger<AuthorApiController> _logger;
        private readonly IMapper _mapper;
        private readonly SignInManager<LoginUser> _signInManager;
        private readonly UserManager<LoginUser> _userManager;
        private readonly IConfiguration _config;

        public AuthorApiController(BookClubContext context,
            IAuthorRepository repository,
            ILogger<AuthorApiController> logger,
            IMapper mapper,
            SignInManager<LoginUser> signInManager,
            UserManager<LoginUser> userManager,
            IConfiguration config)
        {
            _context = context;
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
            _signInManager = signInManager;
            _userManager = userManager;
            _config = config;
        }

        // GET: api/Author
        [HttpGet]
        public async Task<IActionResult> GetAuthors()
        {
            try
            {
                var results = _repository.GetAllAuthors();
                return Ok(results);
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"Problem getting books. {ex.Message}");
                return BadRequest(); // is bad request the proper return here? 
            }
        }

        // GET: api/Author/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Author>> GetAuthor(int id)
        {
            var author = await _context.Authors.FindAsync(id);

            if (author == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<Author, AuthorViewModel>(author));
        }

        // POST: api/Author
        [HttpPost]
        public async Task<IActionResult> CreateAuthor()
        {
          // TODO: Create Author Object and check data for other actions that need to be created
            return null;
        }


        // PUT: api/Author
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAuthor(int id, Author author)
        {
            if (id != author.Id)
            {
                return BadRequest();
            }

            _context.Entry(author).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            { 
             if (!AuthorExists(id))
                {
                    return NotFound();
                }
             else
                {
                    _logger.LogError($"Concurrency Exception. {ex.Message}");
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Problem updating author. {ex.Message}");
                // Bad request here? 
                return BadRequest();
            }

            return NoContent();
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
            return _context.Authors.Any(a => a.Id == id);
        }

        //[Authorize]
        public IActionResult AuthorList()
        {
            var results = _repository.GetAllAuthors();
            return View(results);
        }

    }
}
