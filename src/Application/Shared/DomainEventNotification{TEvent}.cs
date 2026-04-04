using Domain.Shared;
using MediatR;

namespace Application.Shared;

// This wrapper tells MediatR how to carry your pure Domain Event
public class DomainEventNotification<TEvent>(TEvent domainEvent) : INotification where TEvent : IDomainEvent
{
    public TEvent DomainEvent { get; } = domainEvent;
}
