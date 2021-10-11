using BookClub.Data.Entities;
using System.Collections.Generic;

namespace BookClub.Authors
{
    public interface IAuthorRepository
    {
        UserAuthor GetAllUserAuthors();
        IEnumerable<Author> GetAllAuthors();
        bool SaveAll();
        void AddEntity(object model);
    }
}
