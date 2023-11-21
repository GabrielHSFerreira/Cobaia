using System;

namespace Cobaia.Domain.Exceptions
{
    public sealed class EntityNotFoundException : Exception
    {
        public Type? EntityType { get; }
        public Guid EntityId { get; }

        public EntityNotFoundException(Type entityType, Guid entityGuid)
            : base($"Entity {entityType.Name} not found for Id {entityGuid}.")
        {
            EntityType = entityType;
            EntityId = entityGuid;
        }
    }
}