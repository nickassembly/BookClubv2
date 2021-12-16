using BookClub.Data.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookClub.ViewModels
{
    public class AuthorViewModel
    {
        [Required]
        public string Firstname { get; set; }

        [Required]
        public string Lastname { get; set; }
        
        public string Nationality { get; set; }
        
        public string BiographyNotes { get; set; }
        public List<Book> Books { get; set; } = new List<Book>();
        public List<Genre> Genres { get; set; } = new List<Genre>();

        public IList<SelectListItem> GenreList { get; set; }
        public IList<SelectListItem> BookList { get; set; }

        public AuthorViewModel()
        {
            GenreList = new List<SelectListItem>();
            BookList = new List<SelectListItem>();
        }
       
        public List<int> GenreIds { get; set; }
        public List<int> BookIds { get; set; }

    }
}
