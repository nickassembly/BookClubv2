using BookClub.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookClub.ViewModels
{
    public class EmailDetail
    {
        public string RecepientName { get; set; }
        public Author? AuthorDetails { get; set; }
        public Book? BookDetails { get; set; }
    }
}
