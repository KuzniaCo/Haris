using System;
using System.Linq;
using Caliburn.Micro;
using Haris.Core.Infrastructure;
using Haris.Core.Modules;
using Haris.Core.Modules.IntentRecognition.Core;
using Haris.Core.Services.Luis;
using SimpleInjector;

namespace Haris.Core
{
	public class AppCoreBootstrapper
	{
		public Container Container { get; private set; }

		public void Run()
		{
			Container = new Container();
			ConfigureContainer();
			RunInitializers();
		}

		private void ConfigureContainer()
		{
			Container.RegisterSingleton<IEventAggregator>(new EventAggregator {PublicationThreadMarshaller = QueueAsync});
			Container.RegisterSingleton<ILuisUrlProvider, LuisUrlProvider>();
			Container.RegisterSingleton<IIntentRecognizer, LuisIntentRecognizer>();

			var types =
				GetType()
					.Assembly.GetTypes()
					.Where(RegistrationPredicate)
					.ToList();
			Container.RegisterCollection<IHarisModule>(types);
		}

		private bool RegistrationPredicate(Type t)
		{
			return t.IsAbstract == false 
				&& t.GetCustomAttributes(false).All(a => a is DisableModuleAttribute == false)
				&& t.IsClass && t.GetInterfaces().Any(i => i == typeof (IHarisModule));
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