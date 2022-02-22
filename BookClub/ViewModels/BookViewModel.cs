using BookClub.Data.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
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
        public string Category { get; set; }
        public string Description { get; set; }
        public string Identifier { get; set; }
        public string IdentifierType { get; set; }
        public string ImageUrl { get; set; }
        public List<Author> Authors { get; set; } = new List<Author>();
        public List<Genre> Genres { get; set; } = new List<Genre>();
        public IList<SelectListItem> GenreList { get; set; }
        public IList<SelectListItem> AuthorList { get; set; }
        public BookViewModel()
        {
            GenreList = new List<SelectListItem>();
            AuthorList = new List<SelectListItem>();
        }
        public List<int> GenreIds { get; set; }
        public List<int> AuthorIds { get; set; }
    }
}
