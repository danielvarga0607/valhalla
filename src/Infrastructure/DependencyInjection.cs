using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using Valhalla.Application.Common.Interfaces;
using Valhalla.Infrastructure.Persistence;

namespace Valhalla.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services
                .AddDbContext<ValhallaDbContext>(options =>
                {
                    if (!options.IsConfigured)
                    {
                        var connectionString = configuration.GetConnectionString("Database");
                        var migrationAssemblyName = typeof(ValhallaDbContext).Assembly.FullName;
                        options.UseSqlServer(
                            connectionString, sqlServerOptions => sqlServerOptions.MigrationsAssembly(migrationAssemblyName));
                    }
                });

            services.AddScoped<IValhallaDbContext>(provider => provider.GetService<ValhallaDbContext>());

            return services;
        }
    }
}