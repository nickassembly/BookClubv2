using System.Collections.Generic;

namespace BookClub.Data.Entities
{
    public class Publisher
    {
        public int Id { get; set; }
        public string Name { get; set; }

        // Navigation properties
        public ICollection<Book> Books { get; set; }
    }
}
