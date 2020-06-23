using System;
using System.Collections.Generic;
using System.Windows;
using Autofac;
using Caliburn.Micro;
using Hoursly.Common.Extensions.DependencyInjection;
using Hoursly.Common.Helpers;
using Hoursly.Database;
using Hoursly.ViewModels;

namespace Hoursly
{
    public class Bootstrapper : BootstrapperBase
    {
        private IContainer _container;

        public Bootstrapper()
        {
            Initialize();
        }

        protected override void OnStartup(object sender, StartupEventArgs e)
        {
            MigrateDatabase();
            DisplayRootViewFor<ShellViewModel>();
        }

        protected override void Configure()
        {
            var builder = new ContainerBuilder();
            var currentAssembly = typeof(Bootstrapper).Assembly;

            builder.AddCaliburn();
            builder.AddMappers();
            builder.AddDatabase();
            builder.AddSystemUtilities();
            builder.AddRepositories(currentAssembly);
            builder.AddValidators(currentAssembly);
            builder.AddViewModels(currentAssembly);

            _container = builder.Build();
        }

        protected override IEnumerable<object> GetAllInstances(Type service)
        {
            return _container.Resolve(typeof(IEnumerable<>).MakeGenericType(service)) as IEnumerable<object>;
        }

        protected override object GetInstance(Type service, string key)
        {
            if (string.IsNullOrWhiteSpace(key))
            {
                if (_container.IsRegistered(service))
                    return _container.Resolve(service);
            }
            else
            {
                if (_container.IsRegisteredWithKey(key, service)) return _container.ResolveKeyed(key, service);
            }

            throw new InvalidOperationException($"Could not locate any instances of contract {key ?? service.Name}.");
        }

        protected override void BuildUp(object instance)
        {
            _container.InjectProperties(instance);
        }

        private void MigrateDatabase()
        {
            var connectionString = HourslyConfiguration.ConnectionString;
            var databaseMigrator = _container.Resolve<IDatabaseMigrator>();
            databaseMigrator.MigrateDatabase(connectionString);
        }
    }
}