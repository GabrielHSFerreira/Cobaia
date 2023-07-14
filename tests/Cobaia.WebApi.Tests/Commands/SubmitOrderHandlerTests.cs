﻿using Cobaia.WebApi.Commands.SubmitOrder;
using Cobaia.WebApi.Tests.Utils;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using NSubstitute;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Cobaia.WebApi.Tests.Commands
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
            var result = await handler.Handle(new SubmitOrderCommand { OrderId = orderId }, CancellationToken.None);
            var createdOrder = await context.Orders.FindAsync(orderId);

            // Assert
            createdOrder
                .Should()
                .NotBeNull();
            createdOrder!.Id
                .Should()
                .Be(orderId);
            result.CreatedId
                .Should()
                .Be(orderId);
        }
    }
}