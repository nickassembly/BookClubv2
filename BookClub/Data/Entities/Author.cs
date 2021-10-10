using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BookClub.Data.Entities
{
    public class Author
    {
        [Key]
        public int AuthorId { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string AuthorBio { get; set; }
        public DateTime Birthdate { get; set; }
        public ICollection<Book> Books { get; set; }
        public ICollection<Genre> Genres { get; set; }

    }
}