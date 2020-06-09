using System.Reflection;
using Hoursly.Domain.Common;
using Hoursly.Persistance.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Hoursly.Persistance
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistence(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("MasterDatabase");
            AddDbContext<HourslyDbContext>(services, connectionString);

            services.AddScoped(typeof(IBaseEntityRepository<>), typeof(BaseEntityRepository<>));

            return services;
        }

        private static void AddDbContext<TContext>(
            IServiceCollection services,
            string connectionString)
            where TContext : DbContext
        {
            var migrationAssemblyName = Assembly.GetExecutingAssembly().GetName().Name;
            services.AddDbContext<TContext>(options =>
                options.UseSqlite(connectionString,
                    contextOptions => contextOptions.MigrationsAssembly(migrationAssemblyName))
            );
        }
    }
}