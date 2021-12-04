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
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public AuthorBio AuthorBio { get; set; }
        public List<Book> Books { get; set; } = new List<Book>();
        public List<Genre> Genres { get; set; } = new List<Genre>();

    }
}
