using System;

namespace Wemogy.Core.Tests.Reflection.TypeEditorTestData;

public abstract class EntityBase : IEntity<Guid>
{
    public Guid Id { get; set; }

    public DateTime CreatedAt { get; set; }

    protected EntityBase()
    {
        Id = Guid.NewGuid();
        CreatedAt = DateTime.UtcNow;
    }
}
