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
            throw new NotImplementedException();
            //try
            //{
            //    var result = _ctx.Authors.Include(a => a.Books);

            //    return result.ToList();
            //}
            //catch (Exception ex)
            //{
            //    _logger.LogError($"Failed to get all Authors: {ex}");
            //    return null;
            //}
        }

        public UserAuthor GetAllUserAuthors()
        {
            throw new NotImplementedException();
            //try
            //{
            //    var userId = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

            //    var authorList = _ctx.UserAuthors
            //        .Where(u => u.User.Id == userId).Include(a => a.Authors).ThenInclude(b => b.Books).FirstOrDefault();

            //    return authorList;
            //}
            //catch (Exception ex)
            //{
            //    _logger.LogError($"Failed to get all of User Author List: {ex}");
            //    return null;
            //}
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
