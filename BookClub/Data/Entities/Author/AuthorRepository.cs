using BookClub.Generics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookClub.Data.Entities
{
    public class AuthorRepository : RepositoryBase<UserAuthor>, IAuthorRepository
    {
        public AuthorRepository(BookClubContext bookclubContext) : base(bookclubContext)
        {

        }
    }
}
