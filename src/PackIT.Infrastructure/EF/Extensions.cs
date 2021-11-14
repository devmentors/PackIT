using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PackIT.Application.Services;
using PackIT.Domain.Repositories;
using PackIT.Infrastructure.EF.Contexts;
using PackIT.Infrastructure.EF.Options;
using PackIT.Infrastructure.EF.Repositories;
using PackIT.Infrastructure.EF.Services;
using PackIT.Shared.Options;

namespace PackIT.Infrastructure.EF
{
    internal static class Extensions
    {
        public static IServiceCollection AddPostgres(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IPackingListRepository, PostgresPackingListRepository>();
            services.AddScoped<IPackingListReadService, PostgresPackingListReadService>();
            
            var options = configuration.GetOptions<PostgresOptions>("Postgres");
            services.AddDbContext<ReadDbContext>(ctx => 
                ctx.UseNpgsql(options.ConnectionString));
            services.AddDbContext<WriteDbContext>(ctx => 
                ctx.UseNpgsql(options.ConnectionString));

            return services;
        }
    }
}