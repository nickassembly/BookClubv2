using BookClub.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookClub.ViewModels
{
    public class UserBookViewModel
    {
        public int UserBookId { get; set; }
        public LoginUser User { get; set; }
        public ICollection<Book> Books { get; set; }
        public ICollection<Author> Authors { get; set; }

    }
}
