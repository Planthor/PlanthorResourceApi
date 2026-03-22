using System;
using System.Collections.Generic;
using PlanthorWebApi.Domain.Shared;

namespace PlanthorWebApi.Domain;

public class Member : IAggregateRoot, IEntity<Guid>
{
    public Guid Id => throw new NotImplementedException();

    public IReadOnlyList<IDomainEvent> DomainEvents => throw new NotImplementedException();

    public void ClearDomainEvents()
    {
        throw new NotImplementedException();
    }

    public ValidationResult Validate()
    {
        throw new NotSupportedException();
    }
}
