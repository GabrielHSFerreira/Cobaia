using Cobaia.Domain.Exceptions;
using Cobaia.Domain.Orders;
using Cobaia.Persistence.Contexts;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Cobaia.WebApi.Commands.AcceptOrder
{
    public class AcceptOrderHandler : IRequestHandler<AcceptOrderCommand>
    {
        private readonly ILogger<AcceptOrderHandler> _logger;
        private readonly CobaiaContext _context;

        public AcceptOrderHandler(ILogger<AcceptOrderHandler> logger, CobaiaContext context)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task Handle(AcceptOrderCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Accepting order {orderId}", request.OrderId);

            var order = await _context.Orders.FirstOrDefaultAsync(x => x.Id == request.OrderId, cancellationToken);

            if (order == null)
                throw new EntityNotFoundException(typeof(Order), request.OrderId);

            order.Accept();

            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}