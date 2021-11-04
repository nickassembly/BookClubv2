using BookClub.Data;
using BookClub.Data.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace BookClub.Authors
{
    public class AuthorRepository : IAuthorRepository
    {
        private readonly BookClubContext _ctx;
        private readonly ILogger<AuthorRepository> _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AuthorRepository(BookClubContext ctx, ILogger<AuthorRepository> logger, IHttpContextAccessor httpContextAccessor)
        {
            _ctx = ctx;
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
        }

        public IEnumerable<Author> GetAllAuthors()
        {
            try
            {
                var result = _ctx.Authors.ToList();
                return result;
            }
            catch (Exception ex)
            {

                _logger.LogError($"Failed to get all authors. {ex.Message}");
                return null;
            }
        }

        public UserAuthor GetAllUserAuthors()
        {
            try
            {
                // Not sure about this query... very confusing, need to test
                var userId = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

                var userAuthorList = _ctx.Users.Where(u => u.Id == userId).Select(a => a.UserAuthors).FirstOrDefault();

                return userAuthorList.FirstOrDefault();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to get all user authors. {ex.Message}");
                return null;
            }
     
        }

        public bool SaveAll()
        {
            return _ctx.SaveChanges() > 0;
        }

        public void AddEntity(object model)
        {
            _ctx.Add(model);
        }
    }
}
