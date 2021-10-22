using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookClub.Data.Entities
{
    // TODO: Move to folders, change namespace, finish model, create new migration and update
    public class Book
    {
        public int Id { get; set; }
        public string Category { get; set; }
        public double Price { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime PublishDate { get; set; }
        public string Identifier { get; set; }
        public string IdentifierType { get; set; }
       
        // Navigation properties
        public ICollection<BookAuthor> BookAuthors { get; set; }
        public ICollection<GenreBook> GenreBooks { get; set; }
        public int? PublisherId { get; set; }
        public Publisher Publisher { get; set; }
    }
}