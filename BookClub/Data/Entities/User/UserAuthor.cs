using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BookClub.Data.Entities
{
    public class UserAuthor
    {
        public int Id { get; set; }

        // Navigation properties
        public LoginUser User { get; set; }
        public string UserId { get; set; }
        public int AuthorId { get; set; }
        public Author Author { get; set; }

        // TODO: Add bool to track IsSubscribed
        // If true, then the user attached to that user author would be following that author's news feed
        // need to also add column in author's list to show that property

    }
}
