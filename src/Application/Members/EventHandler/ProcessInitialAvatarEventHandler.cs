using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Shared;
using Domain.Members.Events;

namespace Application.Members.EventHandler;

public class ProcessInitialAvatarEventHandler : IDomainEventHandler<MemberRegisteredEvent>
{
    public Task HandleAsync(MemberRegisteredEvent domainEvent, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
