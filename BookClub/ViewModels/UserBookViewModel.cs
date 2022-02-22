using BookClub.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookClub.ViewModels
{
    public class UserBookViewModel
    {
        public LoginUser User { get; set; }
        public IList<Book> Books { get; set; }

        public IList<int> BookIds { get; set; }


    }
}
