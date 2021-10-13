using BookClub.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookClub.ViewModels
{
    public class AuthorViewModel
    {
        public int AuthorId { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string AuthorBio { get; set; }
        public DateTime Birthdate { get; set; }
        public ICollection<Book> Books { get; set; }
        public ICollection<Genre> Genres { get; set; }

    }
}
