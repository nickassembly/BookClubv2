using System.Collections.Generic;

namespace BookClub.Data.Entities
{
    public class Genre
    {
        public int Id { get; set; }
        public string GenreName { get; set; }
        public string GenreDescription { get; set; }

        // Navigation properties
        public ICollection<BookGenre> GenreBooks { get; set; }
        public ICollection<AuthorGenre> GenreAuthors { get; set; }

    }
}
