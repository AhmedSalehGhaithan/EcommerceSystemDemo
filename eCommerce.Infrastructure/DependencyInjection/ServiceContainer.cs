using eCommerce.Application.Services.Interfaces.Logging;
using eCommerce.Domain.Entities;
using eCommerce.Domain.Interfaces;
using eCommerce.Infrastructure.Data;
using eCommerce.Infrastructure.Repositories;
using eCommerce.Infrastructure.Services;
using EntityFramework.Exceptions.SqlServer;
using Microsoft.AspNetCore.Builder;
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
                }).UseExceptionProcessor(),
                ServiceLifetime.Scoped);

            service.AddScoped<IGeneric<Product>, GenericRepository<Product>>();
            service.AddScoped<IGeneric<Category>, GenericRepository<Category>>();
           
            service.AddScoped(typeof(IAppLogger<>),typeof(SerilogLoggerAdapter<>));

            return service;
        }
        /// <summary>
        /// Configures the middleware pipeline to include custom exception handling
        /// for the eCommerce application. This middleware captures exceptions thrown
        /// during request processing and provides a centralized way to handle them,
        /// improving error management and user experience.
        /// </summary>
        /// <param name="app">The IApplicationBuilder used to configure the application's request pipeline.</param>
        /// <returns>The modified IApplicationBuilder, enabling method chaining.</returns>

        public static IApplicationBuilder UseInfrastructureService(this IApplicationBuilder app)
        {
            app.UseMiddleware<ExceptionHandlingMiddleware>();
            return app;
        }
    }
}
