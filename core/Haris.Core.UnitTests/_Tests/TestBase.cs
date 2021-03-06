﻿using NUnit.Framework;
using SimpleInjector;

namespace Haris.Core.UnitTests._Tests
{
	public class TestBase
	{
		protected Container Container;

		[TestFixtureSetUp]
		public virtual void TestFixtureSetUp()
		{
			Container = new Container();
			Container.Options.AllowOverridingRegistrations = true;
		}
	}
}