using FluentValidation.AspNetCore;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

using Valhalla.Web.Filters;

namespace Valhalla.Web.Installers
{
    public class MvcInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            services
                .AddControllers(options => options.Filters.Add(new ApiExceptionFilterAttribute()))
                .AddFluentValidation();

            services.AddSwaggerGen(x =>
            {
                const string version = "v1";
                x.SwaggerDoc(version, new OpenApiInfo
                {
                    Title = "Valhalla API",
                    Version = version
                });
            });
        }
    }
}