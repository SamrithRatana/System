using Microsoft.EntityFrameworkCore;
using ServiceMaintenance.Models;

namespace ServiceMaintenance.Api.Models
{
    public class ServerDbContext : DbContext
    {
        public ServerDbContext(DbContextOptions<ServerDbContext> options)
            : base(options)
        {
        }

        public DbSet<Repairs> Items { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Additional model configuration if needed
        }
    }
}
