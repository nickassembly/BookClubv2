using BookClub.Data.Entities;
using System.Collections.Generic;

namespace BookClub.Data
{
    public interface IUserRepository
    {
        IEnumerable<LoginUser> GetAllUsers();
    }
}