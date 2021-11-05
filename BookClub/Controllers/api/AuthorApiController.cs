using AutoMapper;
using AutoMapper.Configuration;
using BookClub.Data;
using BookClub.Data.Entities;
using BookClub.Generics;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BookClub.Controllers.api
{
    [Route("api/[controller]/[action]")]
   // [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class AuthorApiController : BaseApiController
    {
        private readonly BookClubContext _context;
        
        private readonly IRepository<Author> _repository;
        private readonly IMediator _mediator;

        private readonly ILogger<AuthorApiController> _logger;
        private readonly IMapper _mapper;
        private readonly SignInManager<LoginUser> _signInManager;
        private readonly UserManager<LoginUser> _userManager;
        private readonly IConfiguration _config;

        public AuthorApiController(
            IRepository<Author> repository,
            IMediator mediator,

            BookClubContext context,
            ILogger<AuthorApiController> logger,
            IMapper mapper,
            SignInManager<LoginUser> signInManager,
            UserManager<LoginUser> userManager,
            IConfiguration config)
        {
            _repository = repository;
            _mediator = mediator;

            _context = context;
            _logger = logger;
            _mapper = mapper;
            _signInManager = signInManager;
            _userManager = userManager;
            _config = config;
            _repository = repository;
        }


    }
}
