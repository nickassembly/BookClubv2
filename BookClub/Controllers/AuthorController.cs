using AutoMapper;
using AutoMapper.Configuration;
using BookClub.Apis;
using BookClub.Data;
using BookClub.Data.Entities;
using BookClub.Generics;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace BookClub.Controllers
{
    [Route("api/[controller]/[action]")]
   // [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class AuthorController : BaseApiController
    {
        private readonly BookClubContext _context;
        private readonly ILogger<AuthorController> _logger;
        private readonly SignInManager<LoginUser> _signInManager;
        private readonly UserManager<LoginUser> _userManager;
        private readonly IConfiguration _config;

        private readonly IMediator _mediator;
        private readonly IRepository<Author> _repository;

        public AuthorController(
            BookClubContext context,
            ILogger<AuthorController> logger,
            SignInManager<LoginUser> signInManager,
            UserManager<LoginUser> userManager,
            IConfiguration config,
            IMediator mediator,
            IRepository<Author> repository)
        {

            _context = context;
            _logger = logger;
            _signInManager = signInManager;
            _userManager = userManager;
            _config = config;

            _mediator = mediator;
            _repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> List()
        {
            var result = await _mediator.Send(new AuthorListRequest());

            return Ok(result.Authors);
        }



    }
}
