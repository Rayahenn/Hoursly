using System;
using System.Collections.Generic;
using System.Windows;
using Autofac;
using Caliburn.Micro;
using Hoursly.ViewModels;

namespace Hoursly
{
    public class Bootstrapper : BootstrapperBase
    {
        private static IContainer _container;

        public Bootstrapper()
        {
            Initialize();
        }

        protected override void OnStartup(object sender, StartupEventArgs e)
        {
            DisplayRootViewFor<ShellViewModel>();
        }

        protected override void Configure()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<WindowManager>().AsImplementedInterfaces().SingleInstance();
            builder.RegisterType<EventAggregator>().AsImplementedInterfaces().SingleInstance();
            builder.RegisterType<ShellViewModel>()
                .AsSelf();

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
    }
}