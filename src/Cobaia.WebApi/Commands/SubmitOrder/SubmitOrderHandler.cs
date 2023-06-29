using Cobaia.Domain.Orders;
using Cobaia.Persistence.Contexts;
using Cobaia.WebApi.Models;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Cobaia.WebApi.Commands.SubmitOrder
{
    public class SubmitOrderHandler : IRequestHandler<SubmitOrderCommand, CreatedEntity>
    {
        private readonly ILogger<SubmitOrderHandler> _logger;
        private readonly CobaiaContext _context;

        public SubmitOrderHandler(ILogger<SubmitOrderHandler> logger, CobaiaContext context)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<CreatedEntity> Handle(SubmitOrderCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Creating order {orderId}", request.OrderId);

            var order = new Order(request.OrderId, DateTime.UtcNow);

            _context.Orders.Add(order);
            await _context.SaveChangesAsync(cancellationToken);

            return new CreatedEntity(order.Id, order.CreatedDate);
        }
    }
}