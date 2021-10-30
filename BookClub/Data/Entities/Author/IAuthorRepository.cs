using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookClub.Data.Entities
{
    public interface IAuthorRepository
    {
        // TODO: Add methods to handle data from new data model
        UserAuthor GetAllUserAuthors();
        IEnumerable<Author> GetAllAuthors();

        bool SaveAll();
        void AddEntity(object model);
    }
}
