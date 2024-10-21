using eCommerce.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace eCommerce.Infrastructure.Data
{
    /// <summary>
    /// Represents the application's database context for Entity Framework.
    /// </summary>
    /// <param name="options">Configuration options for the DbContext.</param>
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        /// <summary>
        /// Gets or sets the collection of Products in the database.
        /// </summary>
        public DbSet<Product> Products => Set<Product>();

        /// <summary>
        /// Gets or sets the collection of Categories in the database.
        /// </summary>
        public DbSet<Category> Categories => Set<Category>();
    }
}
