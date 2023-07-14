using Cobaia.WebApi.Models;
using MediatR;
using System;

namespace Cobaia.WebApi.Commands.SubmitOrder
{
    public record SubmitOrderCommand : IRequest<CreatedEntity>
    {
        public Guid OrderId { get; init; }
    }
}