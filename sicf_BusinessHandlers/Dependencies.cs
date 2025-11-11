using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using sicf_BusinessHandlers.BusinessHandlers.Common.Handlers;
using sicf_BusinessHandlers.BusinessHandlers.Conversor;
using sicf_BusinessHandlers.BusinessHandlers.Conversor.Fachadas;

namespace sicf_BusinessHandlers
{
    public static class Dependencies
    {
        public static IServiceCollection AgregarBussinessDependencias(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<FileHandler>();
            services.AddScoped<ConversorProcesoFachada>();
            services.AddScoped<IConversorService, ConversorService>();

            return services;
        }
    }
}
