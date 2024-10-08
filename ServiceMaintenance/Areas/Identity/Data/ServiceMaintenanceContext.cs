    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;
    using ServiceMaintenance.Models;

    namespace ServiceMaintenance.Data;

    public class ServiceMaintenanceContext : IdentityDbContext<ApplicationUser>
    {
        public ServiceMaintenanceContext(DbContextOptions<ServiceMaintenanceContext> options)
            : base(options)
        {
        }
        public DbSet<Message> Messages { get; set; }
      public DbSet<Article> Articles { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Message>()
               .HasOne<ApplicationUser>(a => a.Sender)
               .WithMany(d => d.Messages)
               .HasForeignKey(d => d.UserID)
               .OnDelete(DeleteBehavior.Cascade); // Example of ensuring proper foreign key handling

       

        builder.Entity<ApplicationUser>().ToTable("Users", "security");
            builder.Entity<IdentityRole>().ToTable("Roles", "security");
            builder.Entity<IdentityUserRole<string>>().ToTable("UserRoles", "security");
            builder.Entity<IdentityUserClaim<string>>().ToTable("UserClaims", "security");
            builder.Entity<IdentityUserLogin<string>>().ToTable("UserLogins", "security");
            builder.Entity<IdentityRoleClaim<string>>().ToTable("RoleClaims", "security");
            builder.Entity<IdentityUserToken<string>>().ToTable("UserTokens", "security");
        // Configure primary key for Article if necessary
        builder.Entity<Article>()
            .HasKey(a => a.Id);
    }
    }
