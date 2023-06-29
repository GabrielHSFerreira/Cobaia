using Cobaia.Domain.Orders;
using Microsoft.EntityFrameworkCore;

namespace Cobaia.Persistence.Contexts
{
    public class CobaiaContext : DbContext
    {
        public DbSet<Order> Orders => Set<Order>();

        public CobaiaContext(DbContextOptions options) : base(options) { }
    }
}