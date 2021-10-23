using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookClub.Data.Entities
{
    public class GenreBook
    {
        public int Id { get; set; }

        // Navigation Properties
        public int GenreId { get; set; }
        public Genre Genre { get; set; }
        public int BookId { get; set; }
        public Book Book { get; set; }

    }
}
