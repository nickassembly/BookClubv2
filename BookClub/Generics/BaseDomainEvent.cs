using System;

namespace BookClub.Generics
{
    public class BaseDomainEvent 
    {
        public DateTime DateOccurred { get; protected set; } = DateTime.UtcNow;
    }
}
