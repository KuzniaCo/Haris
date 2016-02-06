using System;
using System.Linq;
using Caliburn.Micro;
using Haris.Core.Infrastructure;
using Haris.Core.Modules;
using SimpleInjector;

namespace Haris.Core
{
	public class AppCoreBootstrapper
	{
		public Container Container { get; private set; }

		public void Run()
		{
			Container = new Container();
			ConfigureKernel();
			RunInitializers();
		}

		private void ConfigureKernel()
		{
			Container.Options.AllowOverridingRegistrations = true;
			Container.RegisterSingleton<IEventAggregator>(new EventAggregator {PublicationThreadMarshaller = QueueAsync});

			var types =
				GetType()
					.Assembly.GetTypes()
					.Where(t => t.IsAbstract == false && t.IsClass && t.GetInterfaces().Any(i => i == typeof (IHarisModule)))
					.ToList();
			Container.RegisterCollection<IHarisModule>(types);
		}

		private void QueueAsync(Action action)
		{
			AsyncActionsQueue.Enqueue(action);
		}

		private void RunInitializers()
		{
			foreach (var module in Container.GetAllInstances<IHarisModule>())
			{
				module.Init();
			}
		}

		public void Shutdown()
		{
			foreach (var module in Container.GetAllInstances<IHarisModule>())
			{
				module.Dispose();
			}
			Container.Dispose();
		}

	}
}