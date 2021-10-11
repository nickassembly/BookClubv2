using AutoMapper;
using BookClub.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

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
    }
}
