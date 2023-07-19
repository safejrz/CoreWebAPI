using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;

namespace CoreWebAPI.Models
{
    public class CoreWebAPIContext : DbContext
    {
        public CoreWebAPIContext(DbContextOptions<CoreWebAPIContext> options) : base(options) { }

        public DbSet<Product> Products { get; set; }
    }
}
