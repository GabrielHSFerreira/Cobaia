using MediatR;
using System;

namespace Cobaia.WebApi.Commands.AcceptOrder
{
    public record AcceptOrderCommand : IRequest
    {
        public Guid OrderId { get; init; }
    }
}