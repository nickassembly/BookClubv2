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
        public int Id { get; set; }

        // Navigation Properties
        public int BookId { get; set; }
        public Book Book { get; set; }
        public string UserId { get; set; }
        public LoginUser User { get; set; }

    }
}