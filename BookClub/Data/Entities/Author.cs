using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BookClub.Data.Entities
{
    public class Author
    {
        [Key]
        public int Id { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }

        // Navigation Properties
        // 1:1 Author -> Authorbio
        public int? AuthorBioId { get; set; }
        public AuthorBio AuthorBio { get; set; }
        public ICollection<Book> Books { get; set; }
        public ICollection<Genre> Genres { get; set; }

    }
}