using BookClub.Generics;
using System.Collections.Generic;

namespace BookClub.Data.Entities
{
    public class Author : BaseEntity, IAggregateRoot
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }

        // Navigation Properties
        // 1:1 Author -> Authorbio
        // 1:N Author --> Books, Author --> Genres ... etc. 
        public int? AuthorBioId { get; set; }
        public AuthorBio AuthorBio { get; set; }
        public ICollection<UserAuthor> UserAuthors { get; set; }
        public ICollection<BookAuthor> BookAuthors { get; set; }
        public ICollection<AuthorGenre> GenreAuthors { get; set; }

    }
}