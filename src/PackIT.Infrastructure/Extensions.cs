using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PackIT.Application.Services;
using PackIT.Infrastructure.EF;
using PackIT.Infrastructure.Logging;
using PackIT.Infrastructure.Services;
using PackIT.Shared.Abstractions.Commands;
using PackIT.Shared.Queries;

namespace PackIT.Infrastructure
{
    public static class Extensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddPostgres(configuration);
            services.AddQueries();
            services.AddSingleton<IWeatherService, DumbWeatherService>();

            services.TryDecorate(typeof(ICommandHandler<>), typeof(LoggingCommandHandlerDecorator<>));
            
            return services;
        }
    }
}