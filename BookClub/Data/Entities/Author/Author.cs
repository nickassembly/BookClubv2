using System.Collections.Generic;

namespace BookClub.Data.Entities
{
    public class Author
    {
        public int Id { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Nationality { get; set; }
        public string BiographyNotes { get; set; }

        public ICollection<UserAuthor> UserAuthors { get; set; }
        public ICollection<BookAuthor> AuthorBooks { get; set; }
        public ICollection<AuthorGenre> AuthorGenres { get; set; }

    }
}