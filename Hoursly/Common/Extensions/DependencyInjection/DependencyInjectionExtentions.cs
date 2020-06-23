using System.Reflection;
using Autofac;
using Caliburn.Micro;
using FluentValidation;
using Hoursly.Common.Messages;
using Hoursly.Database;
using Hoursly.Mappers.Common;
using Hoursly.Repositories.Common;

namespace Hoursly.Common.Extensions.DependencyInjection
{
    public static class DependencyInjectionExtentions
    {
        public static void AddViewModels(this ContainerBuilder builder, Assembly assembly)
        {
            builder.RegisterAssemblyTypes(assembly)
                .Where(t => t.Name.EndsWith("ViewModel"))
                .AsSelf();
        }

        public static void AddRepositories(this ContainerBuilder builder, Assembly assembly)
        {
            builder.RegisterAssemblyTypes(assembly)
                .Where(t => t.Name.EndsWith("Repository"))
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();
        }

        public static void AddMappers(this ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(typeof(IMapper<,>).Assembly)
                .AsClosedTypesOf(typeof(IMapper<,>))
                .SingleInstance();
        }

        public static void AddValidators(this ContainerBuilder builder, Assembly assembly)
        {
            builder.RegisterAssemblyTypes(assembly)
                .Where(t => t.IsClosedTypeOf(typeof(IValidator<>)))
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();
        }

        public static void AddCaliburn(this ContainerBuilder builder)
        {
            builder.RegisterType<WindowManager>().AsImplementedInterfaces().SingleInstance();
            builder.RegisterType<EventAggregator>().AsImplementedInterfaces().SingleInstance();
        }

        public static void AddDatabase(this ContainerBuilder builder)
        {
            builder.RegisterType<DatabaseMigrator>().AsImplementedInterfaces().SingleInstance();
            builder.RegisterType<HourslyDbContex>().AsSelf().InstancePerLifetimeScope();
        }

        public static void AddSystemUtilities(this ContainerBuilder builder)
        {
            builder.RegisterType<SystemMessageSender>().AsImplementedInterfaces().SingleInstance();
        }
    }
}