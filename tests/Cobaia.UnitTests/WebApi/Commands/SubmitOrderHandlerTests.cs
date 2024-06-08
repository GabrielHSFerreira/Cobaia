using Cobaia.UnitTests.WebApi.Utils;
using Cobaia.WebApi.Commands.SubmitOrder;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using NSubstitute;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Cobaia.UnitTests.WebApi.Commands
{
    public class SubmitOrderHandlerTests
    {
        [Fact]
        public async Task Handle_NewOrder_OrderSaved()
        {
            // Arrange
            var context = CobaiaContextFactory.CreateInMemory();
            var handler = new SubmitOrderHandler(
                Substitute.For<ILogger<SubmitOrderHandler>>(),
                context);
            var orderId = Guid.NewGuid();

            // Act
            await handler.Handle(new SubmitOrderCommand { OrderId = orderId }, CancellationToken.None);
            var createdOrder = await context.Orders.FindAsync(orderId);

            // Assert
            createdOrder
                .Should()
                .NotBeNull();
        }

        [Fact]
        public async Task Handle_NewOrder_CreatedWithReceivedId()
        {
            // Arrange
            var context = CobaiaContextFactory.CreateInMemory();
            var handler = new SubmitOrderHandler(
                Substitute.For<ILogger<SubmitOrderHandler>>(),
                context);
            var orderId = Guid.NewGuid();

            // Act
            await handler.Handle(new SubmitOrderCommand { OrderId = orderId }, CancellationToken.None);
            var createdOrder = await context.Orders.FindAsync(orderId);

            // Assert
            createdOrder!.Id
                .Should()
                .Be(orderId);
        }

        [Fact]
        public async Task Handle_NewOrder_ResultWithCreatedOrderId()
        {
            // Arrange
            var context = CobaiaContextFactory.CreateInMemory();
            var handler = new SubmitOrderHandler(
                Substitute.For<ILogger<SubmitOrderHandler>>(),
                context);
            var orderId = Guid.NewGuid();

            // Act
            var result = await handler.Handle(new SubmitOrderCommand { OrderId = orderId }, CancellationToken.None);
            var createdOrder = await context.Orders.FindAsync(orderId);

            // Assert
            result.CreatedId
                .Should()
                .Be(createdOrder!.Id);
        }
    }
}