using System.Threading;
using System.Threading.Tasks;
using Domain.Shared;

namespace Application.Shared;

/// <summary>
///
/// </summary>
/// <typeparam name="TEvent"></typeparam>
public interface IDomainEventHandler<in TEvent> where TEvent : IDomainEvent
{
    /// <summary>
    ///
    /// </summary>
    /// <param name="domainEvent"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task HandleAsync(TEvent domainEvent, CancellationToken cancellationToken);
}
