using System;

namespace Cobaia.WebApi.Models
{
    public record CreatedEntity
    {
        public Guid CreatedId { get; init; }
        public DateTime CreatedDate { get; init; }

        public CreatedEntity(Guid createdId, DateTime createdDate)
        {
            CreatedId = createdId;
            CreatedDate = createdDate;
        }
    }
}