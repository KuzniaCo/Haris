using SimpleInjector;

namespace Haris.Core.UnitTests._Tests
{
	public class TestBase
	{
		protected Container Container;
		public TestBase()
		{
			Container = new Container();
			Container.Options.AllowOverridingRegistrations = true;
		}
	}
}