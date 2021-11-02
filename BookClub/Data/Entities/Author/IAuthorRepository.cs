using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookClub.Data.Entities
{
    public interface IAuthorRepository
    {
        UserAuthor GetAllUserAuthors();
        AuthorGenre GetAllGenreAuthors();
        IEnumerable<Author> GetAllAuthors();
        bool SaveAll();
        void AddEntity(object model);
    }
}
