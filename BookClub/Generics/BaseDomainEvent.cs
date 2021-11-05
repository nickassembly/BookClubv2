using MediatR;
using System;

namespace BookClub.Generics
{
    public class BaseDomainEvent : INotification
    {
        public DateTime DateOccurred { get; protected set; } = DateTime.UtcNow;
    }
}
