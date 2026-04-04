using System;
using System.Threading;
using System.Threading.Tasks;
using Domain.Shared;
using Infrastructure.Context;

namespace Infrastructure.Repositories;

public abstract class BaseRepository<T> : IWriteRepository<T> where T : class, IAggregateRoot
{
    protected readonly PlanthorDbContext Context;

    protected BaseRepository(PlanthorDbContext context)
    {
        ArgumentNullException.ThrowIfNull(context);
        Context = context;
    }

    public virtual async Task<T> AddAsync(T item, CancellationToken cancellationToken)
    {
        await Context.Set<T>().AddAsync(item, cancellationToken);
        return item;
    }

    public virtual Task DeleteAsync(T item, CancellationToken cancellationToken)
    {
        Context.Set<T>().Remove(item);
        return Task.CompletedTask;
    }

    public Task<int> SaveChangesAsync(CancellationToken cancellationToken)
    {
        return Context.SaveChangesAsync(cancellationToken);
    }

    public Task UpdateAsync(T item, CancellationToken cancellationToken)
    {
        Context.Set<T>().Update(item);
        return Task.CompletedTask;
    }
}
