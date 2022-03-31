using BookClub.Data.Entities;
using BookClub.Data.Entities.User;
using System.Collections.Generic;

namespace BookClub.ViewModels
{
    public class LoginUserProfileViewModel
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }

        public List<BookViewModel> UserBookList { get; set; }

        public ICollection<LoginUserFriendship> FriendsOf { get; set; }
        public List<LoginUserFriendship> Friends { get; set; } = new List<LoginUserFriendship>();
        public ICollection<UserBook> UserBooks { get; set; }
        public ICollection<UserAuthor> UserAuthors { get; set; }
  
    }
}
