using Cobaia.Domain.Orders;
using FluentAssertions;
using System;
using Xunit;

namespace Cobaia.UnitTests.Domain.Orders
{
    public class OrderTests
    {
        [Fact]
        public void Accept_StatusChangedToAccepted()
        {
            // Arrange
            var order = new Order(Guid.NewGuid(), DateTime.UtcNow);

            // Act
            order.Accept(DateTime.UtcNow);

            // Assert
            order.Status
                .Should()
                .Be(OrderStatus.Accepted);
        }
    }
}