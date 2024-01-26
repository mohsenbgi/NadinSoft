using Microsoft.Extensions.DependencyInjection;
using NadinSoft.Application.Bus;

namespace NadinSoft.Infra.IoC
{
    public static class DependencyInjector
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            // Domain Bus (Mediator)
            services.AddScoped<IMediatorHandler, InMemoryBus>();
        }
    }
}
