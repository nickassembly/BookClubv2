using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookClub.Data.Entities
{
    public class BookUser
    {
        [Key]
        public int BookUserId { get; set; }
        public ICollection<Book> Books { get; set; }
    }
}