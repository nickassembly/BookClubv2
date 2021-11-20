using BookClub.Apis;
using BookClub.Data.Entities;
using BookClub.Generics;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BookClub.Controllers
{
    [Route("api/[controller]/[action]")]
   // [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class AuthorController : BaseApiController
    {
        private readonly IMediator _mediator;
        private readonly IRepository<Author> _repository;

        public AuthorController(
            IMediator mediator,
            IRepository<Author> repository)
        {
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
