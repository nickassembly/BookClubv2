using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookClub.Data.Entities
{
    public class AuthorCreateRequest : IRequest<AuthorCreateResponse>
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }

        public int? AuthorBioId { get; set; }
        public AuthorBio AuthorBio { get; set; }
        public ICollection<UserAuthor> UserAuthors { get; set; }
        public ICollection<BookAuthor> BookAuthors { get; set; }
        public ICollection<AuthorGenre> GenreAuthors { get; set; }
    }
}
