using Microsoft.Extensions.DependencyInjection;
using Bcp.Test.Transversal.Common;
using Bcp.Test.Infrastructure.Data;
using Bcp.Test.Infrastructure.Repository;
using Bcp.Test.Application.Interface;
using Bcp.Test.Application.Main;
using Bcp.Test.Domain.Interface;
using Bcp.Test.Domain.Core;
using Bcp.Test.Infrastructure.Interface;
using Bcp.Test.Transversal.Logging;
using Microsoft.Extensions.Configuration;

namespace Bcp.Test.Services.WebApi.Modules.Injection
{
    public static class InjectionExtensions
    {
        public static IServiceCollection AddInjection(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IConfiguration>(configuration);
            services.AddSingleton<IConnectionFactory, ConnectionFactory>();
            services.AddScoped<ITipoCambioApplication, TipoCambioApplication>();
            services.AddScoped<ITipoCambioDomain, TipoCambioDomain>();
            services.AddScoped<ITipoCambioRepository, TipoCambioRepository>();
            services.AddScoped<IUsersApplication, UsersApplication>();
            services.AddScoped<IUsersDomain, UsersDomain>();
            services.AddScoped<IUsersRepository, UsersRepository>();
            services.AddScoped(typeof(IAppLogger<>), typeof(LoggerAdapter<>));

            return services;
        }
    }
}
