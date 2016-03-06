using System.IO;
using Haris.DataModel.Luis;
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

			LuisResponseDto obj = null;
			Assert.DoesNotThrow(() => obj = JsonConvert.DeserializeObject<LuisResponseDto>(file));
			Assert.IsNotNull(obj);
		}
	}
}