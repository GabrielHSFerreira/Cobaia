using Cobaia.WebApi.Models;
using Microsoft.EntityFrameworkCore;

namespace Cobaia.WebApi.Persistence
{
    public class CobaiaWebApiContext : DbContext
    {
        public DbSet<Order> Orders => Set<Order>();

        public CobaiaWebApiContext(DbContextOptions options) : base(options) { }
    }
}