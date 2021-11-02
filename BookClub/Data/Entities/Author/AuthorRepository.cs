using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookClub.Data.Entities.Author
{
    public class AuthorRepository : IAuthorRepository
    {
        private readonly BookClubContext _ctx;
        private readonly ILogger<AuthorRepository> _logger;
        private readonly UserManager<LoginUser> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AuthorRepository(BookClubContext ctx, ILogger<AuthorRepository> logger, UserManager<LoginUser> userManager, IHttpContextAccessor httpContextAccessor)
        {
            _ctx = ctx;
            _logger = logger;
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
        }

        public void AddEntity(object model)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Author> GetAllAuthors()
        {
            throw new NotImplementedException();
        }

        public AuthorGenre GetAllGenreAuthors()
        {
            throw new NotImplementedException();
        }

        public UserAuthor GetAllUserAuthors()
        {
            throw new NotImplementedException();
        }

        public bool SaveAll()
        {
            throw new NotImplementedException();
        }
    }
}
