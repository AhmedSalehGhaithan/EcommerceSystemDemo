using eCommerce.Domain.Entities;
using eCommerce.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace eCommerce.Infrastructure.Data
{
    /// <summary>
    /// Represents the application's database context for Entity Framework.
    /// </summary>
    /// <param name="options">Configuration options for the DbContext.</param>
    public class AppDbContext(DbContextOptions<AppDbContext> options) : IdentityDbContext<AppUser>(options)
    {
        /// <summary>
        /// Gets or sets the collection of Products in the database.
        /// </summary>
        public DbSet<Product> Products => Set<Product>();

        /// <summary>
        /// Gets or sets the collection of Categories in the database.
        /// </summary>
        public DbSet<Category> Categories => Set<Category>();
      
        public DbSet<RefreshToken> refreshTokens => Set<RefreshToken>();

        /// <summary>
        /// seeding the role data 
        /// </summary>
        /// <param name="builder"></param>
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<IdentityRole>().HasData(
                new IdentityRole{
                    Id = Guid.NewGuid().ToString(),
                    Name = "Admin",
                    NormalizedName = "ADMIN"
            },
                new IdentityRole
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "User",
                    NormalizedName = "User"
                });
        }
    }
}
