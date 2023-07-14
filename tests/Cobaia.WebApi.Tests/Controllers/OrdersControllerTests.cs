using Cobaia.WebApi.Commands.SubmitOrder;
using Cobaia.WebApi.Controllers;
using Cobaia.WebApi.Models;
using Cobaia.WebApi.Tests.Utils;
using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Cobaia.WebApi.Tests.Controllers
{
    public class OrdersControllerTests
    {
        [Fact]
        public async Task SubmitOrder_OrderCreated_ReturnsCreatedEntity()
        {
            // Arrange
            var sender = Substitute.For<ISender>();
            var request = new SubmitOrderCommand { OrderId = Guid.NewGuid() };
            var createdEntity = new CreatedEntity(request.OrderId, DateTime.UtcNow);
            sender.Send(request).Returns(createdEntity);
            var controller = new OrdersController(
                sender,
                CobaiaContextFactory.CreateInMemory());

            // Act
            var response = await controller.SubmitOrder(request);

            // Assert
            response
                .Should()
                .BeOfType<OkObjectResult>();
            (response as OkObjectResult)!.Value
                .Should()
                .Be(createdEntity);
        }

        [Fact]
        public async Task AcceptOrder_OrderAccepted_ReturnsNoContent()
        {
            // Arrange
            var controller = new OrdersController(
                Substitute.For<ISender>(),
                CobaiaContextFactory.CreateInMemory());

            // Act
            var response = await controller.AcceptOrder(Guid.NewGuid());

            // Assert
            response
                .Should()
                .BeOfType<NoContentResult>();
        }
    }
}