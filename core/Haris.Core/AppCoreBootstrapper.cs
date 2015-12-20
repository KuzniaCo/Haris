using Caliburn.Micro;
using Castle.MicroKernel;
using Castle.MicroKernel.Registration;
using Haris.Core.Modules;

namespace Haris.Core
{
	public class AppCoreBootstrapper
	{
		public IKernel Kernel { get; private set; }

		void Run()
		{
			Kernel = new DefaultKernel();
			ConfigureKernel();
		}

		private void ConfigureKernel()
		{
			Kernel.Register(Component.For<IEventAggregator>().ImplementedBy<EventAggregator>().LifestyleSingleton());
			Kernel.Register(
				Types.FromAssemblyInThisApplication().BasedOn<IHarisModule>().WithService.FromInterface().LifestyleSingleton());
		}
	}
}