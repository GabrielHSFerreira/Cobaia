using Cobaia.WebApi.Persistence;
using Microsoft.EntityFrameworkCore;
using System;

namespace Cobaia.WebApi.Tests.Utils
{
    internal static class CobaiaWebApiContextFactory
    {
        public static CobaiaWebApiContext CreateInMemory()
        {
            var options = new DbContextOptionsBuilder<CobaiaWebApiContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            var context = new CobaiaWebApiContext(options);
            context.Database.EnsureCreated();

            return context;
        }
    }
}