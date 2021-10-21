using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookClub.Data.Entities
{
    public class Book
    {
        [Key]
        public int Id { get; set; }
        public string Category { get; set; }
        public double Price { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime PublishDate { get; set; }
        public string Identifier { get; set; }
        public string IdentifierType { get; set; }
       
        // Navigation properties
        public ICollection<Author> Authors { get; set; }
        public ICollection<Genre> Genres { get; set; }
        public int? PublisherId { get; set; }
        public Publisher Publisher { get; set; }
    }
}