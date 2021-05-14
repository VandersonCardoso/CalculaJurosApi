using CalculaJurosApi.Core.Interfaces;
using CalculaJurosApi.Services;
using Microsoft.Extensions.DependencyInjection;

namespace CalculaJurosApi.IoC
{
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection ResolveDependencies(this IServiceCollection services)
        {
            #region Services
            services.AddScoped<ICalculaService, CalculaService>();
            services.AddScoped<IProjetoService, ProjetoService>();
            #endregion

            return services;
        }
    }
}
