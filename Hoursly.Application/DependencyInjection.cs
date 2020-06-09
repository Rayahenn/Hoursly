using System.Reflection;
using Hoursly.Application.Common.Extensions;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Hoursly.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(
            this IServiceCollection services)
        {
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddDecorators();
            return services;
        }
    }
}