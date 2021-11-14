using Microsoft.Extensions.DependencyInjection;
using PackIT.Domain.Factories;
using PackIT.Domain.Policies;
using PackIT.Shared;
using PackIT.Shared.Commands;

namespace PackIT.Application
{
    public static class Extensions
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddCommands();
            services.AddSingleton<IPackingListFactory, PackingListFactory>();

            services.Scan(b => b.FromAssemblies(typeof(IPackingItemsPolicy).Assembly)
                .AddClasses(c => c.AssignableTo<IPackingItemsPolicy>())
                .AsImplementedInterfaces()
                .WithSingletonLifetime());
            
            return services;
        }
    }
}