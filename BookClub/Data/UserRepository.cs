using BookClub.Data.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BookClub.Data
{
    public class UserRepository : IUserRepository
    {
        private readonly BookClubContext _ctx;
        private readonly ILogger<BookRepository> _logger;
        private readonly UserManager<LoginUser> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserRepository(BookClubContext ctx,
            ILogger<BookRepository> logger,
            UserManager<LoginUser> userManager,
            IHttpContextAccessor httpContextAccessor
        )
        {
            _ctx = ctx;
            _logger = logger;
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
        }
        public IEnumerable<LoginUser> GetAllUsers()
        {
            try
            {
                IEnumerable<LoginUser> userList = _ctx.LoginUsers.ToList();
                return userList;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to get all of User's Books: {ex}");
                return null;
            }
        }
    }
}
