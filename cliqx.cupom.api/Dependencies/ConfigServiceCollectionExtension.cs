using cliqx.cupom.api.Repositories.Contracts;
using cliqx.cupom.api.Repositories;
using Microsoft.EntityFrameworkCore;
using cliqx.cupom.api.services;
using cliqx.cupom.api.services.Imp;
using cliqx.cupom.api.Services;
using cliqx.cupom.api.Services.Imp;

namespace cliqx.cupom.api.Dependencies
{
    public static class ConfigServiceCollectionExtension
    {
        public static IServiceCollection AddMyDependencyGroup(
             this IServiceCollection services)
        {
            services.AddScoped<DataContext>();
            services.AddScoped<ICupomService, CupomService>();
            services.AddScoped<ICupomRepository, CupomRepository>();
            services.AddScoped<ICupomUsoPedidoRepository, CupomUsoPedidoRepository>();
            services.AddScoped<ICupomLimiteCpfRepository, CupomLimiteCpfRepository>();
            services.AddScoped<ICalculoDescontoService, CalculoDescontoService>();

            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", builder => builder
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());
            });

            Console.WriteLine("Dependências carregadas");

            return services;
        }
    }
}
