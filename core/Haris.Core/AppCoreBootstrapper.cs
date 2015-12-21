using System;
using Caliburn.Micro;
using Castle.MicroKernel;
using Castle.MicroKernel.Registration;
using Haris.Core.Infrastructure;
using Haris.Core.Modules;

namespace Haris.Core
{
	public class AppCoreBootstrapper
	{
		public IKernel Kernel { get; private set; }

		public void Run()
		{
			Kernel = new DefaultKernel();
			ConfigureKernel();
			RunInitializers();
		}

		private void ConfigureKernel()
		{
			Kernel.Register(Component.For<IEventAggregator>().Instance(new EventAggregator {PublicationThreadMarshaller = QueueAsync}));
			Kernel.Register(
				Classes.FromAssemblyInThisApplication().BasedOn<IHarisModule>().WithServiceFromInterface().LifestyleSingleton());
		}

		private void QueueAsync(Action action)
		{
			AsyncActionsQueue.Enqueue(action);
		}

		private void RunInitializers()
		{
			foreach (var module in Kernel.ResolveAll<IHarisModule>())
			{
				module.Init();
			}
		}

		public void Shutdown()
		{
			foreach (var module in Kernel.ResolveAll<IHarisModule>())
			{
				module.Dispose();
				Kernel.ReleaseComponent(module);
			}
		}

	}
}