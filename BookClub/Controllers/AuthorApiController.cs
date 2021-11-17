using AutoMapper;
using AutoMapper.Configuration;
using BookClub.Data;
using BookClub.Data.Entities;
using BookClub.Generics;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace BookClub.Controllers
{
    [Route("api/[controller]/[action]")]
   // [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class AuthorApiController : BaseApiController
    {
        private readonly BookClubContext _context;
        private readonly ILogger<AuthorApiController> _logger;
        private readonly SignInManager<LoginUser> _signInManager;
        private readonly UserManager<LoginUser> _userManager;
        private readonly IConfiguration _config;

        public AuthorApiController(
            BookClubContext context,
            ILogger<AuthorApiController> logger,
            SignInManager<LoginUser> signInManager,
            UserManager<LoginUser> userManager,
            IConfiguration config)
        {

            _context = context;
            _logger = logger;
            _signInManager = signInManager;
            _userManager = userManager;
            _config = config;
        }

        // POST: api/Authors
        //[HttpPost]
        //public async Task<IActionResult> Create([FromBody] AuthorCreateRequest request)
        //{
        //    var response = await _mediator.Send(request);

        //    return Ok(response);

        //}

    }
}
