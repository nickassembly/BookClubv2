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
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.Extensions.Configuration;

namespace BookClub.Controllers.api
{
    [Route("api/Book")]
   // [Authorize(AuthenticationSchemes=JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    public class ApiBookController : ControllerBase
    {
        private readonly BookClubContext _context;
        private readonly IBookRepository _repository;
        private readonly ILogger<ApiBookController> _logger;
        private readonly IMapper _mapper;
        private readonly SignInManager<LoginUser> _signInManager;
        private readonly UserManager<LoginUser> _userManager;
        private readonly IConfiguration _config;

        public ApiBookController(ILogger<ApiBookController> logger,
            IBookRepository repository,
            BookClubContext context,
            IMapper mapper,
            SignInManager<LoginUser> signInManager,
            UserManager<LoginUser> userManager,
            IConfiguration config
            )
        {
            _context = context;
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
            _signInManager = signInManager;
            _userManager = userManager;
            _config = config;
        }

        // GET: api/Book
        [HttpGet]
        public IActionResult GetBooks()
        {
            try
            {
                //TODO: User and Bookclub context to retrieve books for  logged in user
                var results = _repository.GetAllBooks();
                return Ok(results);
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
        [HttpPost]
        public async Task<IActionResult> CreateTokenAsync([FromBody] LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var user = await _userManager.FindByNameAsync(model.Username);
                    if (user != null)
                    {
                        var result = await _signInManager.CheckPasswordSignInAsync(user, model.Password, false);
                        if (result.Succeeded)
                        {
                            var claims = Array.Empty<Claim>();
                            {
                                new Claim(JwtRegisteredClaimNames.Sub, user.Email);
                                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString());
                                new Claim(JwtRegisteredClaimNames.UniqueName, user.UserName);
                            }
                            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Tokens:Key"]));
                            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                            var token = new JwtSecurityToken(
                                _config["Tokens:Issuer"],
                                _config["Tokens:Audience"],
                                claims,
                                signingCredentials: creds,
                                expires: DateTime.UtcNow.AddMinutes(120));

                            return Created("", new
                            {
                                token = new JwtSecurityTokenHandler().WriteToken(token),
                                expiration = token.ValidTo
                            });

                        }
                    }

                }
                catch (Exception ex)
                {
                    _logger.LogError($"Failed to create token: {ex}");
                }
            }
            return BadRequest();
        }

    }
}
