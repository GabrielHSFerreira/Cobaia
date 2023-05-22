using System;

namespace Cobaia.WebApi.Models
{
    public record SubmitOrder
    {
        public Guid OrderId { get; set; }
    }
}