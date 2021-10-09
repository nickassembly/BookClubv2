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
        [Required]
        [MinLength(4)]
        public string Title { get; set; }
        public DateTime PublishDate { get; set; }
        public string Format { get; set; }

        public ICollection<Author> authors { get; set; }
    }
}
