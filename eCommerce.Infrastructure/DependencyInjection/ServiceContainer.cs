using eCommerce.Domain.Entities;
using eCommerce.Domain.Interfaces;
using eCommerce.Infrastructure.Data;
using eCommerce.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace eCommerce.Infrastructure.DependencyInjection
{
    /// <summary>
    /// Contains extension methods for configuring the dependency injection container
    /// for the eCommerce application's infrastructure services.
    /// </summary>
    public static class ServiceContainer
    {
        /// <summary>
        /// Registers the application's database context and configures Entity Framework Core
        /// to use SQL Server with the specified connection string.
        /// </summary>
        /// <param name="service">The IServiceCollection to add services to.</param>
        /// <param name="config">The IConfiguration instance used to access application settings.</param>
        /// <returns>The modified IServiceCollection, enabling method chaining.</returns>
        public static IServiceCollection AddInfrastructureService(this IServiceCollection service, IConfiguration config)
        {
            service.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(config.GetConnectionString("defaultConnection"),
                sqlOptions =>
                {
                    sqlOptions.MigrationsAssembly(typeof(AppDbContext).Assembly.FullName);
                    sqlOptions.EnableRetryOnFailure();
                }),
                ServiceLifetime.Scoped);

            service.AddScoped<IGeneric<Product>, GenericRepository<Product>>();
            service.AddScoped<IGeneric<Category>, GenericRepository<Category>>();
            return service;
        }
    }
}
