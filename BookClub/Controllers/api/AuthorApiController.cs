using AutoMapper;
using AutoMapper.Configuration;
using BookClub.Data;
using BookClub.Data.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
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
    public class AuthorApiController : ControllerBase
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
            return null;
        }

        // GET: api/Author/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Author>> GetAuthor(int id)
        {
            return null;
        }

        // POST: api/Author
        [HttpPost]
        public async Task<IActionResult> CreateAuthor()
        {
            // TODO: return object, or response json?
            return null;
        }


        // PUT: api/Author
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAuthor(int id, Author author)
        {
            return null;
        }

        // DELETE: api/Author/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAuthor(int id)
        {
            return null;
        }

    }
}
