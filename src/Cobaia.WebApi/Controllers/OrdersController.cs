using Cobaia.WebApi.Commands.SubmitOrder;
using Cobaia.WebApi.Models;
using Cobaia.WebApi.Persistence;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cobaia.WebApi.Controllers
{
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private const string Route = "v1/orders";

        private readonly ISender _sender;
        private readonly ILogger<OrdersController> _logger;
        private readonly CobaiaWebApiContext _context;

        public OrdersController(ISender sender, ILogger<OrdersController> logger, CobaiaWebApiContext context)
        {
            _sender = sender ?? throw new ArgumentNullException(nameof(sender));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        [HttpPost($"{Route}:submit")]
        [ProducesResponseType(typeof(CreatedEntity), StatusCodes.Status200OK)]
        public async Task<IActionResult> SubmitOrder(SubmitOrderCommand request)
        {
            var response = await _sender.Send(request);
            return Ok(response);
        }

        [HttpGet(Route)]
        [ProducesResponseType(typeof(List<Order>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetOrders()
        {
            return Ok(await _context.Orders.ToListAsync());
        }

        [HttpGet($"{Route}/{{id:guid}}")]
        [ProducesResponseType(typeof(Order), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetOrder(Guid id)
        {
            var order = await _context.Orders.FirstOrDefaultAsync(x => x.Id == id);

            if (order == null)
                return NotFound();

            return Ok(order);
        }

        [HttpPost($"{Route}/{{id:guid}}:accept")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> AcceptOrder(Guid id)
        {
            _logger.LogInformation("Accepting order {orderId}", id);

            var order = await _context.Orders.FirstOrDefaultAsync(x => x.Id == id);

            if (order == null)
                return NotFound();

            order.Accept();
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}