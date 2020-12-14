using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Valhalla.Application.Common.Interfaces;
using Valhalla.Infrastructure.Persistence;

namespace Valhalla.Infrastructure
{
    public static class DependencyInjection
    {
        public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services
                .AddDbContext<ValhallaDbContext>(options =>
                {
                    if (options.IsConfigured) return;
                    const string connectionStringName = "Database";
                    var connectionString = configuration.GetConnectionString(connectionStringName);
                    var migrationAssemblyName = typeof(ValhallaDbContext).Assembly.FullName;
                    options.UseSqlServer(
                        connectionString,
                        sqlServerOptions => sqlServerOptions.MigrationsAssembly(migrationAssemblyName));
                });

            services.AddScoped<IValhallaDbContext>(provider => provider.GetService<ValhallaDbContext>());
        }
    }
}