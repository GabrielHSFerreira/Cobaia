using Cobaia.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using System;

namespace Cobaia.UnitTests.WebApi.Utils
{
    internal static class CobaiaContextFactory
    {
        public static CobaiaContext CreateInMemory()
        {
            var options = new DbContextOptionsBuilder<CobaiaContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            var context = new CobaiaContext(options);
            context.Database.EnsureCreated();

            return context;
        }
    }
}