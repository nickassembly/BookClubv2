using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BookClub.Data.Entities
{
    public class UserAuthor
    {
        [Key]
        public int UserAuthorId { get; set; }
        public LoginUser User { get; set; }
        public ICollection<Author> Authors { get; set; }
    }
}
