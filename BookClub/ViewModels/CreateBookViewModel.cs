using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookClub.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BookClub.ViewModels
{
    public class CreateBookViewModel : BookViewModel
    {

        [BindProperty]
        public Book Book { get; set; }

        public void OnPost()
        {

        }
    }
}
