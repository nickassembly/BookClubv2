using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookClub.Data.Entities
{
    public class UserBook
    {
        [Key]
        public int Id { get; set; }
        public LoginUser User { get; set; }
        public ICollection<Book> Books { get; set; }
    }
}