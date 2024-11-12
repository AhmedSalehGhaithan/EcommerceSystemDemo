using eCommerce.Application.Services.Interfaces.Authentication;
using eCommerce.Application.Services.Interfaces.Cart;
using eCommerce.Application.Services.Interfaces.Logging;
using eCommerce.Domain.Entities;
using eCommerce.Domain.Entities.Identity;
using eCommerce.Domain.Interfaces;
using eCommerce.Infrastructure.Data;
using eCommerce.Infrastructure.Repositories;
using eCommerce.Infrastructure.Repositories.Authentication;
using eCommerce.Infrastructure.Repositories.Cart;
using eCommerce.Infrastructure.Service;
using eCommerce.Infrastructure.Services;
using EntityFramework.Exceptions.SqlServer;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

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
        /// <param name="_config">The IConfiguration instance used to access application settings.</param>
        /// <returns>The modified IServiceCollection, enabling method chaining.</returns>
        public static IServiceCollection AddInfrastructureService(this IServiceCollection service, IConfiguration _config)
        {
            service.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(_config.GetConnectionString("defaultConnection"),
                sqlOptions =>
                {
                    sqlOptions.MigrationsAssembly(typeof(AppDbContext).Assembly.FullName);
                    sqlOptions.EnableRetryOnFailure();
                }).UseExceptionProcessor(),
                ServiceLifetime.Scoped);

            service.AddScoped<IGeneric<Product>, GenericRepository<Product>>();
            service.AddScoped<IGeneric<Category>, GenericRepository<Category>>();
           
            service.AddScoped(typeof(IAppLogger<>),typeof(SerilogLoggerAdapter<>));

            service.AddDefaultIdentity<AppUser>(options =>
            {
                options.SignIn.RequireConfirmedEmail = true;
                options.Tokens.EmailConfirmationTokenProvider = TokenOptions.DefaultEmailProvider;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = true;
                options.Password.RequireDigit = true;
                options.Password.RequiredUniqueChars = 1;
                options.Password.RequiredLength = 8;
            }).AddRoles<IdentityRole>().AddEntityFrameworkStores<AppDbContext>();

            service.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateAudience = true,
                    ValidateIssuer = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    RequireExpirationTime = true,
                    ValidIssuer = _config["JWT:Issuer"],
                    ValidAudience = _config["JWT:Audience"],
                    ClockSkew = TimeSpan.Zero,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWT:Key"]!))
                };
            });
            service.AddScoped<IRoleManager, RoleManagement>();
            service.AddScoped<IUserManager, UserManagement>();
            service.AddScoped<ITokenManager, TokenManagement>();

            service.AddScoped<IPaymentMethod, PaymentMethodRepo>();
            service.AddScoped<IPaymentService, StripePaymentService>();
            service.AddScoped<ICart, CartRepo>();

            Stripe.StripeConfiguration.ApiKey = _config["Stripe:SecretKey"];
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
