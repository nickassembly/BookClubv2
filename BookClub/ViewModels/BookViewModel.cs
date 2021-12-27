using BookClub.Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookClub.ViewModels
{
    public class BookViewModel
    {
        public int BookId { get; set; }
        public string Title { get; set; }
        public DateTime PublishDate { get; set; }
        public string Format { get; set; }
        public IList<Author> Authors { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }
        public string Identifier { get; set; }
        public string IdentifierType { get; set; }
        public string ImageUrl { get; set; }
        public IList<Genre> Genres { get; set; }
        public GoogleBookVolume GoogleBookVolume { get; set; }
    }
}
