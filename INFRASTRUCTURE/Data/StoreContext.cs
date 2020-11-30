using CORE.Entities;
using Microsoft.EntityFrameworkCore;

namespace INFRASTRUCTURE.Data
{
    public class StoreContext : DbContext
    {
        public StoreContext(DbContextOptions<StoreContext> options) : base(options)
        {}

        public DbSet<Product> Products { get; set; }
    }
}