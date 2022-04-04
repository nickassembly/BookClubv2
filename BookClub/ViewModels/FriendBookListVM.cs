using BookClub.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookClub.ViewModels
{
    public class FriendBookListVM
    {
        public List<UserBook> FriendBooks { get; set; }
        public string FriendName { get; set; }
    }
}
