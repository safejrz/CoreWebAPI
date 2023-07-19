using CoreWebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace CoreWebAPI.Data
{
    public class CoreWebAPIContext : DbContext
    {
        private readonly IConfiguration _config;

        //public CoreWebAPIContext(DbContextOptions<CoreWebAPIContext> options) : base(options) { }
        public CoreWebAPIContext(IConfiguration config){
            _config = config;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_config.GetConnectionString("DefaultConnection"));
        }

        public DbSet<Product> Products { get; set; }
    }
}
