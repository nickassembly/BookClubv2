using BookClub.Data.Entities;
using System.Collections.Generic;

namespace BookClub.Apis
{
    public class AuthorListApiModel
    {
        public int Id { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public int? AuthorBioId { get; set; }
        public AuthorBio AuthorBio { get; set; }
        public ICollection<UserAuthor> UserAuthors { get; set; }
        public ICollection<BookAuthor> BookAuthors { get; set; }
        public ICollection<AuthorGenre> GenreAuthors { get; set; }
    }
}
