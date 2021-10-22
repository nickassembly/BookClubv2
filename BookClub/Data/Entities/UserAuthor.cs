using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BookClub.Data.Entities
{
    public class UserAuthor
    {
        public int Id { get; set; }

        // Navigation properties
        public int UserId { get; set; }
        public LoginUser User { get; set; }
        public int AuthorId { get; set; }
        public Author Author { get; set; }


    }
}
