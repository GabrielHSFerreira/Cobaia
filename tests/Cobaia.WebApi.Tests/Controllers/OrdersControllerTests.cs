using Cobaia.WebApi.Commands.SubmitOrder;
using Cobaia.WebApi.Controllers;
using Cobaia.WebApi.Models;
using Cobaia.WebApi.Tests.Utils;
using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
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
                Substitute.For<ILogger<OrdersController>>(),
                CobaiaWebApiContextFactory.CreateInMemory());

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
    }
}