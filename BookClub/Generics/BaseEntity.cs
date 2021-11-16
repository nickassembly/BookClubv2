using System.Collections.Generic;

namespace BookClub.Generics
{
    public class BaseEntity
    {
        public int Id { get; set; }
        public List<BaseDomainEvent> Events = new List<BaseDomainEvent>();
    }
}
