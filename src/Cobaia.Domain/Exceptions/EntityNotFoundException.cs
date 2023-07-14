using System;
using System.Runtime.Serialization;

namespace Cobaia.Domain.Exceptions
{
    [Serializable]
    public sealed class EntityNotFoundException : Exception
    {
        public Type? EntityType { get; }
        public Guid EntityId { get; }

        private EntityNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context) { }

        public EntityNotFoundException(Type entityType, Guid entityGuid)
            : base($"Entity {entityType.Name} not found for Id {entityGuid}.")
        {
            EntityType = entityType;
            EntityId = entityGuid;
        }
    }
}