using Cobaia.Domain.Orders;
using Cobaia.Persistence.Contexts;
using Cobaia.WebApi.Commands.AcceptOrder;
using Cobaia.WebApi.Commands.SubmitOrder;
using Cobaia.WebApi.Models;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        private readonly CobaiaContext _context;

        public OrdersController(ISender sender, CobaiaContext context)
        {
            _sender = sender ?? throw new ArgumentNullException(nameof(sender));
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
            await _sender.Send(new AcceptOrderCommand { OrderId = id });
            return NoContent();
        }
    }
}