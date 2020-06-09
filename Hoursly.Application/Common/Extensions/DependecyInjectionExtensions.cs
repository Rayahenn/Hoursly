using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Hoursly.Application.Common.Extensions
{
    internal static class DependecyInjectionExtensions
    {
        internal static IServiceCollection AddDecorators(this IServiceCollection services)
        {
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(UnitOfWorkDecorator<,>));
            return services;
        }
    }
}