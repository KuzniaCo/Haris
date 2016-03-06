using System.IO;
using Newtonsoft.Json;
using NUnit.Framework;

namespace Haris.Core.UnitTests._Tests
{
	[TestFixture]
	public class IntentRecognitionTests
	{
		[SetUp]
		public void Init()
		{

		}

		[Test]
		public void JsonIsProperlyLoaded()
		{
			var file = File.ReadAllText("TestData/TurnOnTvResponse.txt");

			object obj = null;
			Assert.DoesNotThrow(() => obj = JsonConvert.DeserializeObject(file));
			Assert.IsNotNull(obj);
		}
	}
}