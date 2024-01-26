using MediatR;
using Microsoft.Extensions.DependencyInjection;
using NadinSoft.Application.Behaviors;
using NadinSoft.Domain.Interfaces;
using NadinSoft.Infra.Data.Repositories;

namespace NadinSoft.Infra.IoC
{
    public static class DependencyInjector
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationPipelineBehavior<,>));

            // Repositories
            services.AddScoped<IProductRepository, ProductRepository>();
        }
    }
}
