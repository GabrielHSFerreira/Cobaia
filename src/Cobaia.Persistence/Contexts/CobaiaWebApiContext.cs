using Cobaia.Domain.Orders;
using Microsoft.EntityFrameworkCore;

namespace Cobaia.Persistence.Contexts
{
    public class CobaiaWebApiContext : DbContext
    {
        public DbSet<Order> Orders => Set<Order>();

        public CobaiaWebApiContext(DbContextOptions options) : base(options) { }
    }
}