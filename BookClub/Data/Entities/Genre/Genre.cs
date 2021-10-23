using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookClub.Data.Entities
{
    public class Genre
    {

        public int Id { get; set; }
        public string GenreName { get; set; }
        public string GenreDescription { get; set; }

        // Navigation properties
        public ICollection<GenreBook> GenreBooks { get; set; }
        public ICollection<GenreAuthor> GenreAuthors { get; set; }

    }
}
