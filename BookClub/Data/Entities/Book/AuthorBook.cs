using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookClub.Data.Entities
{
    public class AuthorBook
    {
        public int Id { get; set; }

        // Navigation Properties for book and author
        public int BookId { get; set; }
        public Book Book { get; set; }
        public int AuthorId { get; set; }
        public Author Author { get; set; }

    }
}
