using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookClub.Data.Entities
{
    public class AuthorBio
    {
        public int Id { get; set; }
        public string BiographyDescription { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string PlaceOfBirth { get; set; }
        public string Nationality { get; set; }
        public int AuthorId { get; set; }

        // 1:1 AuthorBio -> Author
        public Author Author { get; set; }
    }
}
