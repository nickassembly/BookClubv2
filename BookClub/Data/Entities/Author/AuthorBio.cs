namespace BookClub.Data.Entities
{
    public class AuthorBio
    {
        public int Id { get; set; }
        public string Nationality { get; set; }
        public string BiographyNotes { get; set; }
        public int AuthorId { get; set; }
        // 1:1 AuthorBio -> Author
        public Author Author { get; set; }
    }
}
