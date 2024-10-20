using eCommerce.Application.Mapping;
using eCommerce.Application.Services.Implementations;
using eCommerce.Application.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace eCommerce.Application.DependencyInjection
{
    /// <summary>
    /// Static class for configuring application services and dependencies.
    /// </summary>
    public static class ServiceContainer
    {
        /// <summary>
        /// Registers application services with the dependency injection container.
        /// </summary>
        /// <param name="services">The IServiceCollection to add services to.</param>
        /// <returns>The updated IServiceCollection.</returns>
        public static IServiceCollection AddApplicationService(this IServiceCollection services)
        {
            // Register AutoMapper with the specified mapping configuration
            services.AddAutoMapper(typeof(MappingConfig));

            // Register product and category services with scoped lifetime
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<ICategoryService, CategoryService>();

            return services;
        }
    }
}
