using CoreWebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace CoreWebAPI.Data
{
    public class CoreWebAPIContext : DbContext
    {
        public CoreWebAPIContext(DbContextOptions<CoreWebAPIContext> options) : base(options) { }

        public DbSet<Product> Products { get; set; }
    }
}
